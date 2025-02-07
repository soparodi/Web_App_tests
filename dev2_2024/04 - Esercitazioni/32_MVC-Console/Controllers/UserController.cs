
class UserController 
{
    private Database _db; // riferimento al modello di database
    private UserView _view; // riferimenti alla view
    public UserController (Database db, UserView view) //costruttore della classe controller che prende in input il modello di database e la view
    {
        _db = db; //inizializzazione del riferimento al modello
        _view = view; // inizializzazione del riferimento alla vista
    }
    public void MainMenu()
    {
        while (true)
        {
            _view.ShowMainMenu(); // visualizzazione del menu principale con metodo servito da view
            var input = _view.GetInput(); // lettura dell'inpit dell'utente con metodo servito da view
            if (input == "1")
            {
                AddUser(); //aggiunta di un utente
            }
            else if (input == "2")
            {
                ShowUsers(); //visualizzazione degli utenti
            }
            else if (input == "3")
            {
                _db.CloseConnection();
                break; //uscita dal programma
            }
        }
    }
    private void AddUser()
    {
      
       var name = _view.GetUserName(); //la vista gestice la richiesta del nome
        _db.AddUser(name); // aggiunta dell' utente al database con metodo servito da database
    }
    private void ShowUsers()
    {
        var users = _db.GetUsers(); //lettura degli utenti dal database
        _view.ShowUsers(users); //visualizzazione degli utenti
    }
}
