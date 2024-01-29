using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Collections.Generic;


namespace StudentRegistration
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate states on page load
                string script = @"function ConfirmDelete() {
                                    return confirm('Are you sure you want to delete this record?');
                                 }";
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmDelete", script, true);
                PopulateStates();
                GetData();
            }
        }
        
        // CREATE OPERATION

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

        // READ OPERATION

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
        private void GetData()
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ID, name, age, state, city, address, number FROM studreg", _connection);
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    // Replace state and city IDs with their corresponding names
                    foreach (DataRow row in dt.Rows)
                    {
                        row["state"] = GetStateNameById(row["state"].ToString());
                        row["city"] = GetCityNameById(row["city"].ToString());
                    }

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }


        
        protected void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    _connection.Open();

                    // Get state ID
                    string stateName = ddlState.SelectedItem.Text;
                    string getStateIdQuery = "SELECT stateid FROM state WHERE statename = @StateName";
                    using (SqlCommand getStateIdCmd = new SqlCommand(getStateIdQuery, _connection))
                    {
                        getStateIdCmd.Parameters.AddWithValue("@StateName", stateName);

                        object result1 = getStateIdCmd.ExecuteScalar();
                        string stateId = result1 != null ? result1.ToString() : null;

                        // Get city ID
                        string cityName = ddlCity.SelectedItem.Text;
                        string getCityIdQuery = "SELECT cityid FROM city WHERE cityname = @CityName";
                        using (SqlCommand getCityIdCmd = new SqlCommand(getCityIdQuery, _connection))
                        {
                            getCityIdCmd.Parameters.AddWithValue("@CityName", cityName);
                            object result2 = getCityIdCmd.ExecuteScalar();
                            string cityId = result2 != null ? result2.ToString() : null;

                            // Insert user details into the database with corresponding state and city IDs
                            string insertQuery = "INSERT INTO studreg (name, age, state, city, address, number) VALUES (@Name, @Age, @State, @City, @Address, @Number)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, _connection))
                            {
                                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                                cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                                cmd.Parameters.AddWithValue("@Number", txtNum.Text);
                                cmd.Parameters.AddWithValue("@State", stateId);
                                cmd.Parameters.AddWithValue("@City", cityId);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                GetData();
                ClearFormFields();

                Response.Write("<script>alert('Student registered successfully!')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        //UPDATE OPERATION

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                // Check if the GridView has rows
                if (e.NewEditIndex >= 0 && e.NewEditIndex < GridView1.Rows.Count)
                {
                    GridViewRow row = GridView1.Rows[e.NewEditIndex];

                    // Check if the cells collection is not null
                    if (row.Cells.Count >= 6)
                    {
                        SetDropDownSelectedValues(ddlState, GetStateIdByName(row.Cells[3].Text));
                        PopulateCities(ddlState.SelectedValue);  // Assuming state information is in the third column (adjust if necessary)

                        // Set the initial value of ddlCity and populate cities
                        SetDropDownSelectedValues(ddlCity, GetCityIdByName(row.Cells[4].Text));

                        // Now, set other TextBox values
                        txtName.Text = row.Cells[1].Text;
                        txtAge.Text = row.Cells[2].Text;
                        txtAdd.Text = row.Cells[5].Text;
                        txtNum.Text = row.Cells[6].Text;

                        // Save the index of the edited row in ViewState
                        ViewState["EditingRow"] = e.NewEditIndex;
                    }
                }
                else
                {
                    Response.Write("Error: Index is out of range.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["EditingRow"] != null)
                {
                    int editingRowIndex = (int)ViewState["EditingRow"];

                    if (editingRowIndex >= 0 && editingRowIndex < GridView1.Rows.Count)
                    {
                        GridViewRow editedRow = GridView1.Rows[editingRowIndex];

                        using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                        {
                            _connection.Open();

                            // Update user details in the database
                            string updateQuery = "UPDATE studreg SET name=@Name, age=@Age, state=@State, city=@City, address=@Address, number=@Number WHERE ID=@Id";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, _connection))
                            {
                                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                                cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                                cmd.Parameters.AddWithValue("@Number", txtNum.Text);

                                // Use the corresponding IDs instead of text values
                                cmd.Parameters.AddWithValue("@State", GetStateIdByName(ddlState.SelectedItem.Text));
                                cmd.Parameters.AddWithValue("@City", GetCityIdByName(ddlCity.SelectedItem.Text));

                                // Get the ID of the row being edited
                                int id = Convert.ToInt32(editedRow.Cells[0].Text);
                                cmd.Parameters.AddWithValue("@Id", id);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        GridView1.EditIndex = -1;
                        ViewState["EditingRow"] = null;
                        GetData();
                        ClearFormFields();

                        // Set the updated values in the dropdowns
                        SetDropDownSelectedValues(ddlState, "- Select State -");
                        SetDropDownSelectedValues(ddlCity, "- Select City -");

                        Response.Write("<script>alert('Student information updated successfully!')</script>");
                    }
                    else
                    {
                        Response.Write("Error: Index is out of range.");
                    }
                }
                else
                {
                    Response.Write("<script>alert('No row is being edited.')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        //DELETE OPERATION
        
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = e.RowIndex;

                if (index >= 0 && index < GridView1.DataKeys.Count)
                {
                    int idToDelete = Convert.ToInt32(GridView1.DataKeys[index].Value);
                    List<int> deletedIds = new List<int> { idToDelete };

                    // Delete selected row
                    DeleteSelectedRows(deletedIds);

                    // Update the GridView after deletion
                    GridView1.EditIndex = -1;
                    GetData();
                    ClearFormFields();

                    // Set the updated values in the dropdowns
                    SetDropDownSelectedValues(ddlState, "- Select State -");
                    SetDropDownSelectedValues(ddlCity, "- Select City -");

                    Response.Write("<script>alert('Selected student deleted successfully!')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Invalid row index!')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        private void DeleteSelectedRows(List<int> deletedIds)
        {
            using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                _connection.Open();

                foreach (int idToDelete in deletedIds)
                {
                    // Delete the record from the database
                    string deleteQuery = "DELETE FROM studreg WHERE ID=@Id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, _connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", idToDelete);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void UpdateIdsInDatabase()
        {
            // You don't need the parameter here since you're not using it in the query
            using (SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                _connection.Open();

                // Use a query to reset the identity column
                string resetIdentityQuery = "DBCC CHECKIDENT('studreg', RESEED, 0)";
                using (SqlCommand cmd = new SqlCommand(resetIdentityQuery, _connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        

    }
}
