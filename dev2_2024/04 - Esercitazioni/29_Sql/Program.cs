//sqlite3 database.db
//.open database.db
//CREATE TABLE categorie (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE);
//CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT, prezzo REAL, quantita INTEGER CHECK (quantita >= 0), id_categoria INTEGER, FOREIGN KEY (id_categoria) REFERENCES categorie(id));
//REAL è un dato generico con approssimazione di due, nel prezzo va a troncare (in questo caso) lo 0 dopo il punto
//INSERT INTO categorie (nome) VALUES ('frutta'), ('verdura'), ('verura');
//DELETE FROM categorie WHERE id = 3;
//INSERT INTO categorie (nome) VALUES ('latticini');
//INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('mele', 1.30, 11, 1) , ('pere', 1.20, 10, 1);
//INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('banane', 1.30, 11, 1) , ('uva', 1.20, -1, 1);  
//Runtime error: CHECK constraint failed: quantita >= 0 (19)
//INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('mele', 1.30, 11, 1) , ('uva', 1.20, 10, 1);   
//INSERT INTO categorie (nome) VALUES ('verdura');
//Runtime error: UNIQUE constraint failed: categorie.nome (19)
//SELECT * FROM prodotti JOIN categorie ON prodotti.id_categoria = categorie.id
//SELECT * FROM prodotti ORDER BY DESC;
///SELECT * FROM prodotti ORDER BY ASC;
//.mod column
//SELECT * FROM PRODOTTI JOIN categorie ON prodotti.id_categoria = categorie.id; 
/*id  nome  prezzo  quantita  id_categoria  id  nome  
--  ----  ------  --------  ------------  --  ------
1   mele  1.3     11        1             1   frutta
2   pere  1.2     10        1             1   frutta
3   mele  1.3     11        1             1   frutta
4   uva   1.2     10        1             1   frutta*/
/*ALTER TABLE prodotti ADD COLUMN disponibile BOOLEAN;
//UPDATE prodotti SET disponibile = 1 WHERE id = 1;
//UPDATE prodotti SET disponibile = false WHERE id = 2;
//.schema prodotti                
//CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT, prezzo REAL, quantita INTEGER CHECK (quantita >= 0), id_categoria INTEGER, disponibile BOOLEAN, FOREIGN KEY (id_categoria) REFERENCES categorie(id));sqlite> .schema categorie
//CREATE TABLE categorie (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE);*/
//.mode markdown
//SELECT * FROM PRODOTTI JOIN categorie ON prodotti.id_categoria = categorie.id;
/*| id | nome | prezzo | quantita | id_categoria | disponibile | id |  nome  |
|----|------|--------|----------|--------------|-------------|----|--------|
| 1  | mele | 1.3    | 11       | 1            | 1           | 1  | frutta |
| 2  | pere | 1.2    | 10       | 1            | 0           | 1  | frutta |
| 3  | mele | 1.3    | 11       | 1            |             | 1  | frutta |
| 4  | uva  | 1.2    | 10       | 1            |             | 1  | frutta |*/
//se faccio .help escono tutti i comandi