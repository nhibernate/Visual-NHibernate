using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Slyce.Common.UnitTesting
{
    [TestFixture]
    public class Specs_For_Observable_Trace_Listener
    {
		[SetUp]
		public void Setup()
		{
			TraceLogger.Enabled = true;
		}

		[TearDown]
		public void TearDown()
		{
			TraceLogger.Enabled = false;
		}

        [Test]
        public void Trace_Statements_Are_Reported()
        {
			ObservableTraceListener tl = new ObservableTraceListener();
            AutoResetEvent waitHandle = new AutoResetEvent(false);
            tl.TraceUpdated += delegate { waitHandle.Set(); };

            Trace.Listeners.Add(tl);
            Trace.TraceWarning("Some Message");

            if(waitHandle.WaitOne(1000, false) == false)
            {
                Assert.Fail("The ObservableTraceListener did not report the update within 1 second.");
            }

            Assert.That(tl.ToString().Contains("Some Message"), Is.True, "The TraceListener did not hold the message given to it." + Environment.NewLine + tl);
        }

        [Test]
        public void Trace_Statements_Are_Reported_In_Order()
        {
            ObservableTraceListener tl = new ObservableTraceListener();
            AutoResetEvent waitHandle = new AutoResetEvent(false);
            tl.TraceUpdated += delegate { waitHandle.Set(); };

            Trace.Listeners.Add(tl);
            const string firstMessage = "First Message";
            Trace.TraceWarning(firstMessage);
            const string secondMessage = "Second Message";
            Trace.TraceWarning(secondMessage);

            if (waitHandle.WaitOne(1000, false) == false)
            {
                Assert.Fail("The ObservableTraceListener did not report the update within 1 second.");
            }

            Assert.That(tl.ToString().Contains(firstMessage), Is.True, "The TraceListener did not hold the first message given to it.");
            Assert.That(tl.ToString().Contains(secondMessage), Is.True, "The TraceListener did not hold the second message given to it.");

            Assert.That(tl.ToString().IndexOf(firstMessage), Is.LessThan(tl.ToString().IndexOf(secondMessage)), "The TraceListener did not hold the messages in the correct order.");
        }

        [Test]
        public void Maximum_Size_Is_Honored()
        {
            ObservableTraceListener tl = new ObservableTraceListener("TraceListener1", 20);
            AutoResetEvent waitHandle = new AutoResetEvent(false);
            tl.TraceUpdated += delegate { waitHandle.Set(); };

            Trace.Listeners.Add(tl);
            const string firstMessage = "1234567890";
            Trace.TraceWarning(firstMessage);

            if (waitHandle.WaitOne(1000, false) == false)
            {
                Assert.Fail("The ObservableTraceListener did not report the update within 1 second.");
            }

            Assert.That(tl.ToString().Length, Is.EqualTo(20), "The TraceListener did not truncate the log.");
        }
    }
}
