using System.Buffers.Binary;
using MOCS.Coms;
using MOCS.Cores.VCU;
using MOCS.Protocals.VehicleControl.VehicleToMOCS;
using Xunit.Sdk;

namespace MOCS.Tests
{
    public class MessageDispatcherTests
    {
        private VSPSInfo _VSPSInfo = new();

        private void OnRecvVSPSStatusMsg(VSPSStatusMsg msg)
        {
            var data = msg.UserData.Span;

            _VSPSInfo.Life = data[0];
            _VSPSInfo.Forward = data[1] == 0x01;
            _VSPSInfo.RelativePos = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(2, 2));
            _VSPSInfo.Speed = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(4, 2));
        }

        [Fact]
        public void Subscribe_SyncHandler_ThrowsArgumentNullException_WhenHandlerIsNull()
        {
            var dispatcher = new MessageDispatcher();
            Assert.Throws<ArgumentNullException>(() => dispatcher.Subscribe<VSPSStatusMsg>(null!));
        }

        [Fact]
        public void Subscribe_AsyncHandler_ThrowsArgumentNullException_WhenHandlerIsNull()
        {
            var dispatcher = new MessageDispatcher();
            Func<VSPSStatusMsg, Task> nullHandler = null!;
            Assert.Throws<ArgumentNullException>(() => dispatcher.Subscribe(nullHandler));
        }

        [Fact]
        public void Subscribe_SyncHandler_IncrementsHandlerCount()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            Assert.Equal(1, dispatcher.HandlerCount);
        }

        [Fact]
        public void Subscribe_AsyncHandler_IncrementsHandlerCount()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => Task.CompletedTask);
            Assert.Equal(1, dispatcher.HandlerCount);
        }

        [Fact]
        public void Subscribe_SameTypeTwice_ReplacesHandler()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            Assert.Equal(1, dispatcher.HandlerCount);
        }

        [Fact]
        public void Subscribe_DifferentTypes_IncreasesHandlerCount()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            dispatcher.Subscribe<OBCMsg>(_ => { });
            Assert.Equal(2, dispatcher.HandlerCount);
        }

        [Fact]
        public async Task Dispatch_InvokesSyncHandler_WithCorrectMessage()
        {
            var dispatcher = new MessageDispatcher();
            var invoked = false;
            byte[] rawContent = [0x00, 0x01, 0x03, 0xE8, 0x00, 0x64];
            ReadOnlyMemory<byte> expectedContent = rawContent;

            dispatcher.Subscribe<VSPSStatusMsg>(msg =>
            {
                invoked = true;
                OnRecvVSPSStatusMsg(msg);
            });

            var testMsg = new VSPSStatusMsg { UserData = expectedContent };
            await dispatcher.Dispatch(testMsg);

            Assert.True(invoked);
            Assert.Equal((byte)0, _VSPSInfo.Life);
            Assert.True(_VSPSInfo.Forward);
            Assert.Equal((ushort)1000, _VSPSInfo.RelativePos);
            Assert.Equal((ushort)100, _VSPSInfo.Speed);
        }

        [Fact]
        public async Task Dispatch_InvokesAsyncHandler_WithCorrectMessage()
        {
            var dispatcher = new MessageDispatcher();
            var invoked = false;
            byte[] rawContent = [0x00, 0x01, 0x03, 0xE8, 0x00, 0x64];
            ReadOnlyMemory<byte> expectedContent = rawContent;

            dispatcher.Subscribe<VSPSStatusMsg>(async msg =>
            {
                invoked = true;
                OnRecvVSPSStatusMsg(msg);
                await Task.Delay(100);
            });

            var testMsg = new VSPSStatusMsg { UserData = expectedContent };
            await dispatcher.Dispatch(testMsg);

            Assert.True(invoked);
            Assert.Equal((byte)0, _VSPSInfo.Life);
            Assert.True(_VSPSInfo.Forward);
            Assert.Equal((ushort)1000, _VSPSInfo.RelativePos);
            Assert.Equal((ushort)100, _VSPSInfo.Speed);
        }

        [Fact]
        public async Task Dispatch_WithNullMessage_ReturnsCompletedTask()
        {
            var dispatcher = new MessageDispatcher();
            var task = dispatcher.Dispatch(null!);
            Assert.True(task.IsCompleted);
            await task;
        }

        [Fact]
        public async Task Dispatch_WithUnsubscribedMessageType_ReturnsCompletedTask()
        {
            var dispatcher = new MessageDispatcher();
            var testMsg = new VSPSStatusMsg();
            var task = dispatcher.Dispatch(testMsg);
            Assert.True(task.IsCompleted);
            await task;
        }

        [Fact]
        public async Task Dispatch_AfterSubscribeAndReplace_InvokesLatestHandler()
        {
            var dispatcher = new MessageDispatcher();
            var firstInvoked = false;
            var secondInvoked = false;

            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
            {
                firstInvoked = true;
            });
            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
            {
                secondInvoked = true;
            });

            await dispatcher.Dispatch(new VSPSStatusMsg());

            Assert.False(firstInvoked);
            Assert.True(secondInvoked);
        }

        [Fact]
        public void UnSubscribe_RemovesHandler_ReturnsTrue()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });

            var result = dispatcher.UnSubscribe<VSPSStatusMsg>();

            Assert.True(result);
            Assert.Equal(0, dispatcher.HandlerCount);
        }

        [Fact]
        public void UnSubscribe_NonExistentType_ReturnsFalse()
        {
            var dispatcher = new MessageDispatcher();
            var result = dispatcher.UnSubscribe<VSPSStatusMsg>();
            Assert.False(result);
        }

        [Fact]
        public async Task UnSubscribe_AfterUnsubscribe_DispatchDoesNotInvoke()
        {
            var dispatcher = new MessageDispatcher();
            var invoked = false;

            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
            {
                invoked = true;
            });
            dispatcher.UnSubscribe<VSPSStatusMsg>();
            await dispatcher.Dispatch(new VSPSStatusMsg());

            Assert.False(invoked);
        }

        [Fact]
        public void IsSubscribed_ReturnsTrueForSubscribedType()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            Assert.True(dispatcher.IsSubscribed<VSPSStatusMsg>());
        }

        [Fact]
        public void IsSubscribed_ReturnsFalseForNonSubscribedType()
        {
            var dispatcher = new MessageDispatcher();
            Assert.False(dispatcher.IsSubscribed<VSPSStatusMsg>());
        }

        [Fact]
        public void IsSubscribed_ReturnsFalseAfterUnsubscribe()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            dispatcher.UnSubscribe<VSPSStatusMsg>();
            Assert.False(dispatcher.IsSubscribed<VSPSStatusMsg>());
        }

        [Fact]
        public void Clear_RemovesAllHandlers()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            dispatcher.Subscribe<OBCMsg>(_ => { });

            dispatcher.Clear();

            Assert.Equal(0, dispatcher.HandlerCount);
            Assert.False(dispatcher.IsSubscribed<VSPSStatusMsg>());
            Assert.False(dispatcher.IsSubscribed<OBCMsg>());
        }

        [Fact]
        public async Task Dispatch_MultipleMessageTypes_InvokesCorrectHandlers()
        {
            var dispatcher = new MessageDispatcher();
            var testMsgInvoked = false;
            var anotherMsgInvoked = false;

            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
            {
                testMsgInvoked = true;
            });
            dispatcher.Subscribe<OBCMsg>(_ =>
            {
                anotherMsgInvoked = true;
            });

            await dispatcher.Dispatch(new VSPSStatusMsg());
            Assert.True(testMsgInvoked);
            Assert.False(anotherMsgInvoked);

            testMsgInvoked = false;
            await dispatcher.Dispatch(new OBCMsg());
            Assert.False(testMsgInvoked);
            Assert.True(anotherMsgInvoked);
        }

        [Fact]
        public async Task Dispatch_HandlerThrowsException_PropagatesException()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
                throw new InvalidOperationException("Test exception")
            );

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await dispatcher.Dispatch(new VSPSStatusMsg())
            );
        }

        [Fact]
        public async Task Dispatch_AsyncHandlerThrowsException_PropagatesException()
        {
            var dispatcher = new MessageDispatcher();
            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
            {
                throw new InvalidOperationException("Async test exception");
            });

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await dispatcher.Dispatch(new VSPSStatusMsg())
            );
        }

        [Fact]
        public void HandlerCount_InitiallyZero()
        {
            var dispatcher = new MessageDispatcher();
            Assert.Equal(0, dispatcher.HandlerCount);
        }

        [Fact]
        public void HandlerCount_ReflectsSubscriptionsAndUnsubscriptions()
        {
            var dispatcher = new MessageDispatcher();

            dispatcher.Subscribe<VSPSStatusMsg>(_ => { });
            Assert.Equal(1, dispatcher.HandlerCount);

            dispatcher.Subscribe<OBCMsg>(_ => { });
            Assert.Equal(2, dispatcher.HandlerCount);

            dispatcher.UnSubscribe<VSPSStatusMsg>();
            Assert.Equal(1, dispatcher.HandlerCount);

            dispatcher.Clear();
            Assert.Equal(0, dispatcher.HandlerCount);
        }

        [Fact]
        public async Task ConcurrentDispatch_ThreadSafe()
        {
            var dispatcher = new MessageDispatcher();
            var counter = 0;
            var lockObj = new object();

            dispatcher.Subscribe<VSPSStatusMsg>(_ =>
            {
                lock (lockObj)
                {
                    counter++;
                }
            });

            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => dispatcher.Dispatch(new VSPSStatusMsg()));
            }

            await Task.WhenAll(tasks);
            Assert.Equal(100, counter);
        }

        [Fact]
        public async Task ConcurrentSubscribe_ThreadSafe()
        {
            var dispatcher = new MessageDispatcher();
            var tasks = new Task[100];

            for (int i = 0; i < tasks.Length; i++)
            {
                int index = i;
                tasks[i] = Task.Run(() => dispatcher.Subscribe<VSPSStatusMsg>(_ => { }));
            }

            await Task.WhenAll(tasks);
            Assert.Equal(1, dispatcher.HandlerCount);
        }
    }
}
