using Model;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using View;

namespace Controller
{
    public class UserController
    {

        FormLogin fl;
        Form1 f;
        RegisterForm fr;
        AdminController ac;
        MainRepository mr;
        FormInteractive fi;
        InteractiveController ic;
        Thread thread;

        public UserController()
        {
            fl = new FormLogin();
            f = new Form1();
            mr = new MainRepository();
            ac = new AdminController(mr, fl, f);
            InitListeners();
            InitProgram();
        }

        public void InitProgram()
        {
            CustomFont();
            thread = new Thread(ac.populatePeople);
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            thread.Interrupt();
            Application.Run(fl);
        }


        public void InitListeners()
        {
            fl.loginRegisterButton.Click += LoginRegisterButton_Click;
            fl.ButtonLogin.Click += ButtonLogin_Click;
            fl.showPassLogin.CheckedChanged += ShowPassLogin_CheckedChanged;
        }

        private void ShowPassLogin_CheckedChanged(object sender, EventArgs e)
        {
            ShowPassOnCheckedChanged(fl.showPassLogin, fl.textBox2);
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            int exist = CheckUser();
            if (exist == 1)
            {

                fl.Hide();
                ic = new InteractiveController(mr, fl);

            }
            else if (exist == 0)
            {
                fl.Hide();
                try
                {
                    ac.populatePeopleList();
                    f.Show();
                }
                catch (ObjectDisposedException)
                {
                    f = new Form1();
                    ac.f = f;
                    ac.InitListeners();
                    ac.populatePeopleList();
                    f.Show();
                }          
            }
            else
            {
                MessageBox.Show("Incorrect user or password!");
            }
            fl.textBox1.Text = "";
            fl.textBox2.Text = "";
        }

        private int CheckUser()
        {
            string nom = fl.textBox1.Text;
            string pass = fl.textBox2.Text;

            string HashNom = PasswordEncryptation(nom);
            string HashPass = PasswordEncryptation(pass);

            int exist = mr.ExistUser(HashNom, HashPass);

            return exist;
        }

        private void ShowPassCheck2_CheckedChanged(object sender, EventArgs e)
        {
            ShowPassOnCheckedChanged(fr.showPassCheck2, fr.RepeatPasswordUserText);
        }

        private void ShowwPassCheck1_CheckedChanged(object sender, EventArgs e)
        {
            ShowPassOnCheckedChanged(fr.showPassCheck1, fr.PasswordUserText);
        }

        private void ShowPassOnCheckedChanged(CheckBox checbox, TextBox text)
        {
            if (checbox.Checked)
            {
                text.PasswordChar = '\0';
            }
            else if (!checbox.Checked)
            {
                text.PasswordChar = '*';
            }

        }
        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            fl.Show();
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            //Agafem les variables del form corresponent
            string nom = fr.nameUserText.Text;
            string pass = fr.PasswordUserText.Text;
            string repeatPass = fr.RepeatPasswordUserText.Text;
            string type = fr.TypeUserText.Text;

            string Encryptedpass = null;
            string EncryptedName = null;

            bool typeBool = false;
            if (type.Equals("Usuari"))
            {
                typeBool = true;
            }

            if (pass.Equals(repeatPass))
            {
                Encryptedpass = PasswordEncryptation(pass);
                EncryptedName = PasswordEncryptation(nom);
                AddUser(EncryptedName, Encryptedpass, typeBool);
            }
            else
            {
                MessageBox.Show("Password is not equal.");
            }

            //Tanquem la vista del FormulariRegistre i obrim el de Login

            fr.Close();
            fl.Show();
        }

        private string PasswordEncryptation(string pass)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(pass));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        private void LoginRegisterButton_Click(object sender, EventArgs e)
        {
            InitListenersForm2();
            OpenRegisterForm();

        }

        private void InitListenersForm2()
        {
            fr = new RegisterForm();
            CustomFont2();

            fr.ButtonRegister.Click += ButtonRegister_Click;
            fr.FormClosed += F2_FormClosed;
            fr.showPassCheck1.CheckedChanged += ShowwPassCheck1_CheckedChanged;
            fr.showPassCheck2.CheckedChanged += ShowPassCheck2_CheckedChanged;
        }

        private void OpenRegisterForm()
        {
            fl.Hide();
            fr.Show();
        }

        public void AddUser(string nom, string pass, bool typeBool)
        {
            mr.AddUser(nom, pass, typeBool);
        }

        public void CustomFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.CustomFont1.CustomFont.Length;
            byte[] fontdata = Properties.CustomFont1.CustomFont;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            fl.loginLabelName.Font = new Font(pfc.Families[0], 11);
            fl.loginLabelPassword.Font = new Font(pfc.Families[0], 11);
            fl.ButtonLogin.Font = new Font(pfc.Families[0], 11);
            fl.loginRegisterButton.Font = new Font(pfc.Families[0], 11);

        }

        public void CustomFont2()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.CustomFont1.CustomFont.Length;
            byte[] fontdata = Properties.CustomFont1.CustomFont;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            fr.labelNomUser.Font = new Font(pfc.Families[0], 11);
            fr.labelPassUser.Font = new Font(pfc.Families[0], 11);
            fr.labelRPassUser.Font = new Font(pfc.Families[0], 11);
            fr.labelTypeUser.Font = new Font(pfc.Families[0], 11);
            fr.ButtonRegister.Font = new Font(pfc.Families[0], 11);

        }

    }
}
