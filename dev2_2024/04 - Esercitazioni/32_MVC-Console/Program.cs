using System.Data.SQLite;
var db = new Database(); // modello di database
var view = new UserView(db); //modello di vista c è db tra parentesi perchè la vista deve avere accesso al database
var controller = new UserController(db, view); // modello di controller che deve avere accesso al database e alla vista
controller.MainMenu(); // metodo per gestire il menu principale
