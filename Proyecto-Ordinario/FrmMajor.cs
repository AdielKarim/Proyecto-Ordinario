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
    public partial class FrmMajor : MetroFramework.Forms.MetroForm
    {
        public FrmMajor()
        {
            InitializeComponent();
        }

        private void FrmMajor_Load(object sender, EventArgs e)
        {
            using (DataContext dataContext = new DataContext())
            {
                majorBindingSource.DataSource = dataContext.Majors.ToList();
            }
            pnlDatos.Enabled = false;
            Major major = majorBindingSource.Current as Major;
            if (major != null && major.Photo != null)
                pctPhoto.Image = Image.FromFile(major.Photo);
            else
                pctPhoto.Image = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = true;
            pctPhoto.Image = null;
            majorBindingSource.Add(new Major());
            majorBindingSource.MoveLast();
            txtName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = false;
            majorBindingSource.ResetBindings(false);
            FrmMajor_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = true;
            txtName.Focus();
            Major major = majorBindingSource.Current as Major;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MetroFramework.MetroMessageBox.Show(this, "Quieres eliminar a la carrera") == DialogResult.OK)
            {
                using (DataContext dataContext = new DataContext())
                {
                    Major major = majorBindingSource.Current as Major;
                    if (major != null)
                    {
                        if (dataContext.Entry<Major>(major).State == EntityState.Detached)
                            dataContext.Set<Major>().Attach(major);
                        dataContext.Entry<Major>(major).State = EntityState.Deleted;
                        dataContext.SaveChanges();
                        MetroFramework.MetroMessageBox.Show(this, "Carrera Eliminada");
                        majorBindingSource.RemoveCurrent();
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
                Major major = majorBindingSource.Current as Major;
                if (major != null)
                {
                    if (dataContext.Entry<Major>(major).State == EntityState.Detached)
                        dataContext.Set<Major>().Attach(major);
                    if (major.Id == 0)
                        dataContext.Entry<Major>(major).State = EntityState.Added;
                    else
                        dataContext.Entry<Major>(major).State = EntityState.Modified;
                    dataContext.SaveChanges();
                    MetroFramework.MetroMessageBox.Show(this, "Datos de la carrera guardados");
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
                    Major major = majorBindingSource.Current as Major;
                    if (major != null)
                        major.Photo = ofd.FileName;
                }
            }
        }
    }
}
