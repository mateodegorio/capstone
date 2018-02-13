using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finaltesting
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
        }

        private void enrollment_Click(object sender, EventArgs e)
        {
            enrolls Enroller = new enrolls();
            Enroller.OnTemplate += this.OnTemplate;
            Enroller.Show();
            this.Hide();
        }

        private void verification_Click(object sender, EventArgs e)
        {
            this.Hide();
            verify Verifier = new verify();
            Verifier.Verify(Template);
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke((Action)(delegate ()
            {
                Template = template;
                //VerifyingButton.Enabled = SaveButton.Enabled  = (Template != null);
                if (Template != null)
                    MessageBox.Show("The Fingerprint template is ready for verification and saving", "Fingerprint Enrollment");
                else
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
            }));
        }

        private DPFP.Template Template;

        private void mainform_Load(object sender, EventArgs e)
        {

        }
    }
}
