using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Messages;
using NServiceBus;
using ReadModel;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        private readonly IBus _bus;

        public Form1()
        {
            InitializeComponent();

            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);

            _bus = Configure.With()
                .Log4Net()
                .DefaultBuilder()
                .XmlSerializer()
                .MsmqTransport()
                .IsTransactional(false)
                .PurgeOnStartup(false)
                .UnicastBus()
                .ImpersonateSender(false)
                .CreateBus()
                .Start();
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            _bus.Send(new CreateCustomer(Guid.NewGuid(), textBox1.Text, textBox2.Text));
        }
        */

        private void Form1_Load(object sender, EventArgs e)
        {
            var context = new CQRSExampleEntities();
            dataGridView1.DataSource = new BindingList<Customer>((from x in context.Customers select x).ToList());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var customer = dataGridView1.Rows[e.RowIndex].DataBoundItem as Customer;
            if (customer.Id == Guid.Empty)
            {
                customer.Id = Guid.NewGuid();
                _bus.Send(new CreateCustomer(customer.Id, "", ""));
            }

            var cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            switch (e.ColumnIndex)
            {
                case 0:
                    _bus.Send(new ChangeCustomerName(customer.Id, cellValue.ToString()));//.Register<Reply>(x => { MessageBox.Show(x.Message); });
                    break;
                case 1:
                    _bus.Send(new ChangeCustomerEmailAddress(customer.Id, cellValue.ToString()));
                    break;
                case 2:
                    if (Convert.ToBoolean(cellValue))
                    {
                        _bus.Send(new BlackListEmailAddress(customer.Id));
                    }
                    else
                    {
                        _bus.Send(new UnblacklistEmailAddress(customer.Id));
                    }
                    break;
            }
        }

    }
}
