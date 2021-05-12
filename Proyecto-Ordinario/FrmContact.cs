using Proyecto_Ordinario.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Ordinario
{
    public partial class FrmContact : MetroFramework.Forms.MetroForm
    {
        public FrmContact()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = true;
            pctPhoto.Image = null;
            contactBindingSource.Add(new Contact());
            contactBindingSource.MoveLast();
            txtFirstName.Focus();
        }

        private void FrmContact_Load(object sender, EventArgs e)
        {
            using (DataContext dataContext = new DataContext())
            {
                contactBindingSource.DataSource = dataContext.Contacts.ToList();
            }
            pnlDatos.Enabled = false;
            Contact contact = contactBindingSource.Current as Contact;
            if (contact != null && contact.Photo != null)
                pctPhoto.Image = Image.FromFile(contact.Photo);
            else
                pctPhoto.Image = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = false;
            contactBindingSource.ResetBindings(false);
            FrmContact_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = true;
            txtFirstName.Focus();
            Contact contact = contactBindingSource.Current as Contact;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MetroFramework.MetroMessageBox.Show(this, "Quieres eliminar al Contacto") == DialogResult.OK)
            {
                using (DataContext dataContext = new DataContext())
                {
                    Contact contact = contactBindingSource.Current as Contact;
                    if (contact != null)
                    {
                        if (dataContext.Entry<Contact>(contact).State == EntityState.Detached)
                            dataContext.Set<Contact>().Attach(contact);
                        dataContext.Entry<Contact>(contact).State = EntityState.Deleted;
                        dataContext.SaveChanges();
                        MetroFramework.MetroMessageBox.Show(this, "Contacto Eliminado");
                        contactBindingSource.RemoveCurrent();
                        pctPhoto.Image = null;
                        pnlDatos.Enabled = false;
                    }
                }
            }
        }
    

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DataContext dataContext = new DataContext())
            {
                Contact contact = contactBindingSource.Current as Contact;
                if (contact != null)
                {
                    if (dataContext.Entry<Contact>(contact).State == EntityState.Detached)
                        dataContext.Set<Contact>().Attach(contact);
                    if (contact.Id == 0)
                        dataContext.Entry<Contact>(contact).State = EntityState.Added;
                    else
                        dataContext.Entry<Contact>(contact).State = EntityState.Modified;
                    dataContext.SaveChanges();
                    MetroFramework.MetroMessageBox.Show(this, "Datos del Contacto guardados");
                    grdDatos.Refresh();
                    pnlDatos.Enabled = false;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Necesito los archivos JPG|*.jpg|Todos los archivos|*.*"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pctPhoto.Image = Image.FromFile(ofd.FileName);
                    Contact contact = contactBindingSource.Current as Contact;
                    if (contact != null)
                        contact.Photo = ofd.FileName;
                }
            }
        }
    }
}
