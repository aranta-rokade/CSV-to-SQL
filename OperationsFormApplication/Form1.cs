using System;
using System.Data;
using System.Windows.Forms;
using SQLOperations;

namespace OperationsFormApplication
{
    public partial class Form1 : Form
    {
        DataTable dtSource;
        int PageCount;
        int maxRec;
        int pageSize;
        int currentPage;
        int recNo;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            dtSource = CollectionToSQL.Select();
        }

        ////CRUD

        private void button1_Click_1(object sender, EventArgs e)
        {
            //CollectionToSQL.WriteCollectionToSQL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = CollectionToSQL.Select();
            dtSource = CollectionToSQL.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowAffected = 0;
            try
            {
                int id = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                int billNo = int.Parse(textBox3.Text);
                string email = textBox4.Text;

                rowAffected = CollectionToSQL.Insert(id, name, billNo, email);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (rowAffected == 1)
                {
                    MessageBox.Show("Row Inserted");
                }
                else
                {
                    MessageBox.Show("Oops! An error occured");
                }
                dataGridView.DataSource = CollectionToSQL.Select();
                dtSource = CollectionToSQL.Select();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowAffected = 0;
            try
            {
                int id = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                int billNo = int.Parse(textBox3.Text);
                string email = textBox4.Text;

                rowAffected = CollectionToSQL.Update(id, name, billNo, email);

            }
            catch (Exception ex)
            { }
            finally
            {
                if (rowAffected == 1)
                {
                    MessageBox.Show("Row Updated");
                }
                else
                {
                    MessageBox.Show("Oops! An error occured");
                }
                dataGridView.DataSource = CollectionToSQL.Select();
                dtSource = CollectionToSQL.Select();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
             int rowAffected = 0;
             try
             {
                 int id = int.Parse(textBox1.Text);

                 rowAffected = CollectionToSQL.Delete(id);
             }
             catch (Exception ex)
             {

             }
             finally
             {
                 if (rowAffected == 1)
                 {
                     MessageBox.Show("Row Deleted");
                 }
                 else
                 {
                     MessageBox.Show("Oops! An error occured");
                 }
                 dataGridView.DataSource = CollectionToSQL.Select();
                 dtSource = CollectionToSQL.Select();
             }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        ////PAGING

        private void LoadPage()
        {
            int i;
            int startRec;
            int endRec;
            DataTable dtTemp;

            //Clone the source table to create a temporary table.
            dtTemp = dtSource.Clone();

            if (currentPage == PageCount)
            {
                endRec = maxRec;
            }
            else
            {
                endRec = pageSize * currentPage;
            }
            startRec = recNo;

            //Copy rows from the source table to fill the temporary table.
            for (i = startRec; i < endRec; i++)
            {
                dtTemp.ImportRow(dtSource.Rows[i]);
                recNo += 1;
            }
            dataGridView.DataSource = dtTemp;
            DisplayPageInfo();
        }

        private void DisplayPageInfo()
        {
            txtDisplayPageNo.Text = "Page " + currentPage.ToString() + "/ " + PageCount.ToString();
        }

        private bool CheckFillButton()
        {
            // Check if the user clicks the "Fill Grid" button.
            if (pageSize == 0)
            {
                MessageBox.Show("Set the Page Size, and then click the Fill Grid button!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnFillGrid_Click(object sender, System.EventArgs e)
        {
            // Set the start and max records. 
            pageSize = Convert.ToInt32(txtPageSize.Text);
            maxRec = dtSource.Rows.Count;
            PageCount = maxRec / pageSize;

            //Adjust the page number if the last page contains a partial page.
            if ((maxRec % pageSize) > 0)
            {
                PageCount += 1;
            }

            // Initial seeings
            currentPage = 1;
            recNo = 0;

            // Display the content of the current page.
            LoadPage();
        }

        private void btnFirstPage_Click(object sender, System.EventArgs e)
        {
            if (CheckFillButton() == false)
            {
                return;
            }

            //Check if you are already at the first page.
            if (currentPage == 1)
            {
                MessageBox.Show("You are at the First Page!");
                return;
            }

            currentPage = 1;
            recNo = 0;
            LoadPage();
        }

        private void btnNextPage_Click(object sender, System.EventArgs e)
        {
            //If the user did not click the "Fill Grid" button, then return.
            if (CheckFillButton() == false)
            {
                return;
            }

            //Check if the user clicks the "Fill Grid" button.
            if (pageSize == 0)
            {
                MessageBox.Show("Set the Page Size, and then click the Fill Grid button!");
                return;
            }

            currentPage += 1;
            if (currentPage > PageCount)
            {
                currentPage = PageCount;
                //Check if you are already at the last page.
                if (recNo == maxRec)
                {
                    MessageBox.Show("You are at the Last Page!");
                    return;
                }
            }
            LoadPage();
        }

        private void btnPreviousPage_Click(object sender, System.EventArgs e)
        {
            if (CheckFillButton() == false)
            {
                return;
            }

            if (currentPage == PageCount)
            {
                recNo = pageSize * (currentPage - 2);
            }

            currentPage -= 1;
            //Check if you are already at the first page.
            if (currentPage < 1)
            {
                MessageBox.Show("You are at the First Page!");
                currentPage = 1;
                return;
            }
            else
            {
                recNo = pageSize * (currentPage - 1);
            }
            LoadPage();
        }

        private void btnLastPage_Click(object sender, System.EventArgs e)
        {
            if (CheckFillButton() == false)
            {
                return;
            }

            //Check if you are already at the last page.
            if (recNo == maxRec)
            {
                MessageBox.Show("You are at the Last Page!");
                return;
            }
            currentPage = PageCount;
            recNo = pageSize * (currentPage - 1);
            LoadPage();
        }
    }
}
