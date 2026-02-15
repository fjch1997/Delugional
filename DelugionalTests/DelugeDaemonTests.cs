using Delugional.Daemon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelugionalTests
{
    [TestClass]
    public class DelugeDaemonTests
    {
        [TestMethod]
        public void Default()
        {
            var daemon = DelugeDaemon.Default;

            Assert.IsNotNull(daemon);
        }

        [TestMethod]
        public void StartStopRunning()
        {
            var daemon = DelugeDaemon.Default;

            if (daemon.Running)
            {
                daemon.Stop();
            }

            Assert.IsFalse(daemon.Running);

            daemon.Start();

            Assert.IsTrue(daemon.Running);

            DelugeDaemon.Default.Stop();

            Assert.IsFalse(daemon.Running);
        }
    }
}
