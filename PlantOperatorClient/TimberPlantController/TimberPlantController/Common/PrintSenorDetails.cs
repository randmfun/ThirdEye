using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using PrintDialog = System.Windows.Forms.PrintDialog;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace TimberPlantController.Common
{
    public class PrintSenorDetails
    {
        private System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
        #region Member Variables
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height

        private System.Windows.Controls.DataGrid dataGridView1;

        #endregion

        public void Print()
        {
            var pd = new PrintDialog();
            var pdoc = new PrintDocument();

            dataGridView1 = BatchList.CurrentDataGrid;

            pdoc.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);
            pdoc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            DialogResult result = pd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    pdoc.Print();
                }
            } 

        }

        public void PrintPreview()
        {
            var pdoc = new PrintDocument();

            dataGridView1 = BatchList.CurrentDataGrid;

            pdoc.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);
            pdoc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintPreviewDialog pp = new PrintPreviewDialog();
            pp.Document = pdoc;
            pp.ShowDialog();

        }

        #region Begin Print Event Handler
        /// <summary>
        /// Handles the begin print event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += (int)dgvGridCol.Width.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Print Page Event
        /// <summary>
        /// Handles the print page event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridColumn GridCol in dataGridView1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width.Value /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));
                        iHeaderHeight =(int)(e.Graphics.MeasureString(GridCol.Header.ToString(), 
                                        new Font(FontFamily.GenericSerif, 13), 
                                        iTmpWidth).Height) + 11; ;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }

                var lstGridRows = GetDataGridRows(dataGridView1);
                //Loop till all the grid rows not get printed
                while (iRow <= lstGridRows.Count- 1)
                {
                    var GridRow = lstGridRows[iRow];

                    //Set the cell height
                    iCellHeight = 25;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Sensor Details", new Font(FontFamily.GenericSerif, 12),
                                                  Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                                                                      e.Graphics.MeasureString(
                                                                                          "Customer Summary",
                                                                                          new Font(
                                                                                              FontFamily.GenericSerif,
                                                                                              12), e.MarginBounds.Width)
                                                                                          .Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(FontFamily.GenericSerif, 12),
                                                  Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                                                                        e.Graphics.MeasureString(
                                                                                            strDate,
                                                                                            new Font(
                                                                                                FontFamily.GenericSerif,
                                                                                                12),
                                                                                            e.MarginBounds.Width).Width),
                                                  e.MarginBounds.Top -
                                                  e.Graphics.MeasureString("Sensor Details",
                                                                           new Font(new Font(FontFamily.GenericSerif,
                                                                                             12), FontStyle.Bold),
                                                                           e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridColumn GridCol in dataGridView1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                                         new System.Drawing.Rectangle((int) arrColumnLefts[iCount],
                                                                                      iTopMargin,
                                                                                      (int) arrColumnWidths[iCount],
                                                                                      iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                                         new System.Drawing.Rectangle((int) arrColumnLefts[iCount],
                                                                                      iTopMargin,
                                                                                      (int) arrColumnWidths[iCount],
                                                                                      iHeaderHeight));

                                e.Graphics.DrawString(GridCol.Header.ToString(),
                                                      new Font(FontFamily.GenericSerif, 12),
                                                      new SolidBrush(Color.Black),
                                                      new System.Drawing.RectangleF((int) arrColumnLefts[iCount],
                                                                                    iTopMargin,
                                                                                    (int) arrColumnWidths[iCount],
                                                                                    iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridColumn column in dataGridView1.Columns)
                        {
                            if (column.GetCellContent(GridRow) is TextBlock)
                            {
                                TextBlock cellContent = column.GetCellContent(GridRow) as TextBlock;
                                e.Graphics.DrawString(cellContent.Text, new Font(FontFamily.GenericSerif, 13),
                                                      new SolidBrush(Color.Black),
                                                      new RectangleF((int) arrColumnLefts[iCount], (float) iTopMargin,
                                                                     (int) arrColumnWidths[iCount], (float) iCellHeight),
                                                      strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int) arrColumnLefts[iCount],
                                                                               iTopMargin, (int) arrColumnWidths[iCount],
                                                                               iCellHeight));

                            iCount++;
                        }
                    
                }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public List<DataGridRow> GetDataGridRows(System.Windows.Controls.DataGrid grid)
        {
            var retList =new List<DataGridRow>();
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) 
                return retList;

            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as System.Windows.Controls.DataGridRow;
                if (null != row)
                {
                    retList.Add(row);
                }
            }

            return retList;
        }

        #endregion

  }
}
