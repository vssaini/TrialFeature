using System;
using System.Windows.Forms;

namespace TrialFeature
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            CheckForTrial();
        }

        private void btnShowDataFromRegistry_Click(object sender, EventArgs e)
        {
            try
            {
                var dateValue = Security.GetKeyValue(DateType.FirstUse);
                if (string.IsNullOrEmpty(dateValue)) return;

                var date = Convert.ToDateTime(dateValue);

                txtMessage.Text = string.Format("Date: {0}", date);
            }
            catch (Exception exc)
            {
                txtMessage.Text = exc.Message;
            }
        }

        private void btnShowLastDate_Click(object sender, EventArgs e)
        {
            try
            {
                var dateValue = Security.GetKeyValue(DateType.LastUse);
                if (string.IsNullOrEmpty(dateValue)) return;

                var date = Convert.ToDateTime(dateValue);

                txtMessage.Text = string.Format("Date: {0}", date);
            }
            catch (Exception exc)
            {
                txtMessage.Text = exc.Message;
            }
        }

        /// <summary>
        /// Check if the current software in use is Trial or not
        /// </summary>
        private void CheckForTrial()
        {
            // 1. Get key value to check if exists or not
            var firstUseDateKey = Security.GetKeyValue(DateType.FirstUse);

            // 2.1. If first use date key not exists, then create it
            if (firstUseDateKey == null)
            {
                string errorMessage;
                Security.SaveFirstUseDate(out errorMessage);

                txtMessage.Text = "Trial going on";

                if (errorMessage != null)
                    txtMessage.Text = errorMessage;
            }
            else
            {
                // 2.2. If exists, then create last date use key
                var lastUseDateKey = Security.GetKeyValue(DateType.LastUse);

                if (lastUseDateKey == null)
                {
                    string errorMessage;
                    Security.SaveLastUseDate(out errorMessage);

                    txtMessage.Text = "Trial going on";

                    if (errorMessage != null)
                        txtMessage.Text = errorMessage;
                }
                else
                {
                    // 2.3. If system date less than last used date, mark trial expired
                    var systemDate = DateTime.Now;
                    var lastUseDate = Convert.ToDateTime(lastUseDateKey);

                    if (systemDate < lastUseDate)
                    {
                        txtMessage.Text = "Trial Expired";
                    }
                    else
                    {
                        var daysPassed = (systemDate - lastUseDate).Days;
                        if (daysPassed > 30)
                        {
                            txtMessage.Text = "Trial Expired";
                        }
                        else
                        {
                            // 2.4. Else save system date as last used date
                            string errorMessage;
                            Security.SaveLastUseDate(out errorMessage);

                            txtMessage.Text = "Trial going on";

                            if (errorMessage != null)
                                txtMessage.Text = errorMessage;
                        }
                    }
                }
            }
        }
    }
}
