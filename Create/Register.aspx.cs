using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace StudentRegistration
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate states on page load
                PopulateStates();
                
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate cities based on the selected state
            PopulateCities(ddlState.SelectedValue);
        }

        private void PopulateStates()
        {
            DataTable statesTable = GetStates();

            // Populate states dropdown
            ddlState.DataSource = statesTable;
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "stateid";
            ddlState.DataBind();

            // Add default item
            ddlState.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Select State -", ""));
        }

        private void PopulateCities(string selectedState)
        {
            DataTable citiesTable = GetCitiesByState(selectedState);

            // Populate cities dropdown
            ddlCity.DataSource = citiesTable;
            ddlCity.DataTextField = "cityname";
            ddlCity.DataValueField = "cityid";
            ddlCity.DataBind();

            // Add default item
            ddlCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Select City -", ""));
        }

        private DataTable GetStates()
        {
            DataTable statesTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT stateid, statename FROM state";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(statesTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Console.WriteLine("Error: " + ex.ToString());
            }

            return statesTable;
        }

        private DataTable GetCitiesByState(string stateId)
        {
            DataTable citiesTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT cityid, cityname FROM city WHERE stateid = @stateId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@stateId", stateId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(citiesTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Console.WriteLine("Error: " + ex.ToString());
            }

            return citiesTable;
        }
        
        

        // Add these helper methods
        private string GetStateNameById(string stateId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT statename FROM state WHERE stateid = @StateId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StateId", stateId);
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Response.Write("Error: " + ex.ToString());
                return null;
            }
        }

        private string GetCityNameById(string cityId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT cityname FROM city WHERE cityid = @CityId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CityId", cityId);
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Response.Write("Error: " + ex.ToString());
                return null;
            }
        }

        private string GetStateIdByName(string stateName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT stateid FROM state WHERE statename = @StateName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StateName", stateName);
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Response.Write("Error: " + ex.ToString());
                return null;
            }
        }

        private string GetCityIdByName(string cityName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT cityid FROM city WHERE cityname = @CityName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CityName", cityName);
                        object result = command.ExecuteScalar();
                        return result != null ? result.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, throw, etc.)
                Response.Write("Error: " + ex.ToString());
                return null;
            }
        }

        private void SetDropDownSelectedValues(DropDownList ddl, string selectedValue)
        {
            // Find the ListItem by value
            ListItem item = ddl.Items.FindByValue(selectedValue);

            // If the item is not found, add a default item with the stored value
            if (item == null)
            {
                ddl.Items.Insert(0, new ListItem(selectedValue, selectedValue));
                item = ddl.Items.FindByValue(selectedValue);
            }

            // Set the found or added item as selected
            if (item != null)
            {
                // Clear existing selection only if it's not the same as the selected value
                if (ddl.SelectedValue != selectedValue)
                {
                    ddl.ClearSelection();
                }

                item.Selected = true;
            }
        }
        private void ClearFormFields()
        {
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            txtAdd.Text = string.Empty;
            txtNum.Text = string.Empty;
            ddlCity.Items.Clear();
            ddlCity.Items.Add(new ListItem("- Select City -", ""));
        }
    }
}