﻿

using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

//ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());
//FindCustomer(CreateConnection());
//DisplayProduct(CreateConnection());
//DisplayProductWithCategory(CreateConnection());
InsertCustomers(CreateConnection());

static SQLiteConnection CreateConnection()


{
    SQLiteConnection connection = new SQLiteConnection("Data Source=bar.db; Version = 3; New = true; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found");
    }

    return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);  
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB}");
    }

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name: ");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name: ");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
    $"VALUES ('{fName}', '{lName}', '{dob}')";

   int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

    ReadData(myConnection);

}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer: ");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery(); 
    Console.WriteLine($"{rowRemoved} was removed from the table customer. ");

    ReadData(myConnection);
}

    static void FindCustomer(SQLiteConnection myConnection)
    {
        SQLiteDataReader reader;
        SQLiteCommand command;
        string searchName;
        Console.WriteLine("Enter a first name to display customer data:");

        searchName = Console.ReadLine();
        command = myConnection.CreateCommand();
        command.CommandText = $"SELECT customer.rowid, customer.firstName, customer.lastName, status.statusType " +
        $"FROM customerStatus " +
        $"JOIN customer ON customer.rowid = customerStatus.customerId " +
        $"JOIN status ON status.rowid = customerStatus.statusId " +
        $"WHERE firstname LIKE '{searchName}'";
        reader = command.ExecuteReader();

        while
         (reader.Read())
        {
            string readerRowid = reader["rowid"].ToString();
            string readerStringName = reader.GetString(1);
            string readerStringLastName = reader.GetString(2);
            string readerStringStatus = reader.GetString(3);
            Console.WriteLine($"Search result: ID: {readerRowid}. {readerStringName} {readerStringLastName}. Status: {readerStringStatus}");
        }

        myConnection.Close();
    }
static void DisplayProduct(SQLiteConnection myConnection)
{

    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT rowid, productName, price FROM product";
    reader = command.ExecuteReader();


    while (reader.Read())
    {

        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(
1
);
        int readerProductPrice = reader.GetInt32(
0
); //hinna tüüp andmebaasis on int, nii et siin loeme andmebaasis ka int-tüüpi andmeid

        Console.WriteLine($"{readerRowid}. {readerProductName}. Price: {readerProductPrice}");
    }

    myConnection.Close();
}

static void DisplayProductWithCategory(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT product.rowid, product.productName, ProductCategory.CategoryName FROM product " +
        "JOIN ProductCategory ON ProductCategory.rowid = Product.CategoryId";
    reader = command.
ExecuteReader
();


    while
     (reader.
    Read
    ())
    {

        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(
1
);
        string readerProductCategory = reader.GetString(
2
);

        Console.WriteLine($"{readerRowid}. {readerProductName}. Category: {readerProductCategory}");
    }

    myConnection
    .
    Close
    ();
}

 static void InsertCustomers(SQLiteConnection myConnection)
{

    SQLiteCommand command;
    string fName, lName;

    Console.WriteLine("First name:");
    fName = Console.ReadLine();

    Console.WriteLine("Last name:");
    lName = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName) VALUES('{fName}', '{lName}')";


int rowsInserted = command.ExecuteNonQuery();

    Console.WriteLine($"{rowsInserted} new row has been inserted.");
    


}