using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceTotal
{
    public partial class frmInvoiceTotal : Form
	{
		public frmInvoiceTotal()
		{
			InitializeComponent();
		}

        // TODO: declare class variables for array and list here
        decimal[] arrayInvoice = new decimal[5]; // Declares array and sets it to 5 
        int arrayIndex = 0; //Starts the count at zero

        List<decimal> numbers = new List<decimal>(); // Declares the list and its properties

        private void btnCalculate_Click(object sender, EventArgs e)
		{
            try
            {

                if (txtSubtotal.Text == "")
                {
                    MessageBox.Show(
                        "Subtotal is a required field.", "Entry Error");
                }
                else
                {
			        decimal subtotal = Decimal.Parse(txtSubtotal.Text);
                    if (subtotal > 0 && subtotal < 10000)
                    {
                        decimal discountPercent = 0m;
                        if (subtotal >= 500)
                            discountPercent = .2m;
                        else if (subtotal >= 250 & subtotal < 500)
                            discountPercent = .15m;
                        else if (subtotal >= 100 & subtotal < 250)
                            discountPercent = .1m;
                        decimal discountAmount = subtotal * discountPercent;
			            decimal invoiceTotal = subtotal - discountAmount;

                        discountAmount = Math.Round(discountAmount, 2);
                        invoiceTotal = Math.Round(invoiceTotal, 2);

                        txtDiscountPercent.Text = discountPercent.ToString("p1");
                        txtDiscountAmount.Text = discountAmount.ToString();
                        txtTotal.Text = invoiceTotal.ToString();

                        if (arrayIndex < arrayInvoice.Length) //Only allows the predetermined numbers of invoices (here 5)
                        {
                            arrayInvoice[arrayIndex] = invoiceTotal; //Assigns to array (invoiceTotal)
                            arrayIndex++; //increments invoices 
                            numbers.Add(invoiceTotal); //Adds the totals of the invoices 
                        }
                        else MessageBox.Show("Index out of range", "Array Index Error"); //Error message for more than 5 invoices
                    }
                    else
                    {
                        MessageBox.Show(  //Error for out of range
                            "Subtotal must be greater than 0 and less than 10,000.", 
                            "Entry Error");
                    }
                }
            }
            catch (FormatException) //Catch statement for the format exception. 
            {
                MessageBox.Show(
                    "Please enter a valid number for the Subtotal field.", 
                    "Entry Error");
            }
            txtSubtotal.Focus(); 
        }

		private void btnExit_Click(object sender, EventArgs e)
		{
            // TODO: add code that displays dialog boxes here
            decimal sumA = 0;
            decimal sumList = 0;
            string numberAString = "";
            string numbersListString = "";

            numbers.Sort(); //Sorts the list
            foreach(decimal invoice in numbers) //Builds a string for every decimal value called 'invoice' in 'numbers'
            {
                if (invoice != 0) // As long as display string is not zero, it build s the string
                {
                    numbersListString += invoice + "\n"; // Print statement
                    sumList = +invoice;
                }
            }
            decimal avgList = sumList / numbers.Count(); //Gets the average by dividing the sum by the list count

            Array.Sort(arrayInvoice);  //Sorts the array
            for (int i= 0; i< arrayInvoice.Length; i++)
            {
                if (arrayInvoice[i] != 0)
                {
                    sumA += arrayInvoice[i];
                    numberAString += arrayInvoice[i] + " \n";
                }
            }
            decimal average = sumA / arrayInvoice.Length;

            //Display message for the Array
            MessageBox.Show(numberAString + "\n" + "Sum: " + sumA + " \n" + "Average: " + average +
                " \n", "Array Invoice List");


            //Dispplay message for the List
            MessageBox.Show(numbersListString + "\n" + "Sum List:" + sumList + "\n" + "Average List"
                + avgList + "\n", "List Invoice Totals");
           
                     
            
                            
            this.Close();
		}

	}
}