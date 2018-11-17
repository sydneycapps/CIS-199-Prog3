//B3697
//Program 3
//CIS 199-75
//November 9, 2017
//This program is an alternative solution to program 2 that 
//uses parallel arrays and range matching to determine registration times.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "November 3";   // 1st day of registration
            const string DAY2 = "November 6";   // 2nd day of registration
            const string DAY3 = "November 7";   // 3rd day of registration
            const string DAY4 = "November 8";   // 4th day of registration
            const string DAY5 = "November 9";   // 5th day of registration
            const string DAY6 = "November 10";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?

            int index;                // Array index for search
            bool found = false;       // Looks for a found match

            // Parallel arrays for range matching
            char[] lowerLimitsUpperClass = { 'A', 'E', 'J', 'P', 'T' };       // Lower limits of last name letter for upperclass
            string[] upperClassTimes = { TIME2, TIME3, TIME4, TIME5, TIME1 }; // Upperclass times

            char[] lowerLimitsLowerClass = { 'A', 'C', 'E', 'G', 'J', 'M', 'P', 'R', 'T', 'W' };                 // Lower limits of last name letter for lowerclass
            string[] lowerClassTimes = { TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2 }; // Lowerclass times

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                        index = lowerLimitsUpperClass.Length - 1; // Start from end of array
                        while(index >= 0 && !found)               // Upperclass while loop to determine their registration time
                        {
                            if (lastNameLetterCh >= lowerLimitsUpperClass[index])
                                found = true;
                            else
                                --index;
                        }

                        if (found)
                            timeStr = upperClassTimes[index];
                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }

                        index = lowerLimitsLowerClass.Length - 1; // Start from end of array
                        while(index >= 0 && !found)               // Lowerclass while loop to determine their registration time
                        {
                            if (lastNameLetterCh >= lowerLimitsLowerClass[index])
                                found = true;
                            else --index;
                        }
                        if (found)
                            timeStr = lowerClassTimes[index];
                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
}
