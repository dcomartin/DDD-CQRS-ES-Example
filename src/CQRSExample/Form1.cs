using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NServiceBus;

namespace CQRSExample
{
    public class TestMessage
    {
        public string MyMessage { get; set; }
    }

    public class ExampleMessageHandler : IHandleMessages<TestMessage>
    {
        public void Handle(TestMessage message)
        {
            Console.WriteLine("I got an example message... now what?");
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var senderBus = Configure.With()
              .XmlSerializer()
              .MsmqTransport()
                .IsTransactional(false)
                .PurgeOnStartup(false)
              .UnicastBus()
                .ImpersonateSender(false)
                .Log4Net()
              .CreateBus()
              .Start();

            var receiverBus = Configure.With()
              .XmlSerializer()
              .MsmqTransport()
                .IsTransactional(false)
                .PurgeOnStartup(false)
                                .Log4Net()
                .UnicastBus()
                .ImpersonateSender(false)
                .LoadMessageHandlers()
              .CreateBus()
              .Start();

            senderBus.Send(new TestMessage() {MyMessage = "Hello World!"});
        }
    }
}
