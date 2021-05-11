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
    public partial class FrmStudent : MetroFramework.Forms.MetroForm
    {
        public FrmStudent()
        {
            InitializeComponent();
        }

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            using (DataContext dataContext = new DataContext())
            {
                studentBindingSource.DataSource = dataContext.Students.ToList();
            }
            pnlDatos.Enabled = false;
            Student student = studentBindingSource.Current as Student;
            if (student != null && student.Photo != null)
                pctPhoto.Image = Image.FromFile(student.Photo);
            else
                pctPhoto.Image = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = true;
            pctPhoto.Image = null;
            studentBindingSource.Add(new Student());
            studentBindingSource.MoveLast();
            txtFirstName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = false;
            studentBindingSource.ResetBindings(false);
            FrmStudent_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            pnlDatos.Enabled = true;
            txtFirstName.Focus();
            Student student = studentBindingSource.Current as Student;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MetroFramework.MetroMessageBox.Show(this, "Quieres eliminar al Estuduante") == DialogResult.OK)
            {
                using (DataContext dataContext = new DataContext())
                {
                    Student student = studentBindingSource.Current as Student;
                    if (student != null)
                    {
                        if (dataContext.Entry<Student>(student).State == EntityState.Detached)
                            dataContext.Set<Student>().Attach(student);
                        dataContext.Entry<Student>(student).State = EntityState.Deleted;
                        dataContext.SaveChanges();
                        MetroFramework.MetroMessageBox.Show(this, "Alumno Eliminado");
                        studentBindingSource.RemoveCurrent();
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
                Student student = studentBindingSource.Current as Student;
                if (student != null)
                {
                    if (dataContext.Entry<Student>(student).State == EntityState.Detached)
                        dataContext.Set<Student>().Attach(student);
                    if (student.Id == 0)
                        dataContext.Entry<Student>(student).State = EntityState.Added;
                    else
                        dataContext.Entry<Student>(student).State = EntityState.Modified;
                    dataContext.SaveChanges();
                    MetroFramework.MetroMessageBox.Show(this, "Datos del estudiante guardados");
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
                    Student student = studentBindingSource.Current as Student;
                    if (student != null)
                        student.Photo = ofd.FileName;
                }
            }
        }
    }
}
