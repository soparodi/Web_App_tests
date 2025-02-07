## UTILIZZO MD

# TITOLO PRINCIPALE

## Sottotitolo

### Titolo paragrafo

>esempio citazione

esempio di__grassetto__ o **bold**

esempio di _italic_

esempio di elenco
---

#### Sottotitolo paragrafo


# Esempio di elenco puntato
[link]
---
- primo
    - sottoelenco
- secondo
- terzo
# Esempio di elenco numerato
1. primo
2. secondo
3. terzo
    4. quarto
        5. quinto
            6. sesto

## Esempio di check

- [x] grewd
- [ ] primo
- [ ] secondo
- [x] terzo

# Esempio di codice
```
  git status
  git add
  git commit
 ```

```c#
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, world!");
    }
}

/*
note per i collaboratori
*/
```

**esempio di link relativo**


[link a pagina 2](02_link.md)

[link a paina web] (https://github.com/annapintoanita/dev2_2024/tree/main/04%20-%20Assignment/01_indovina-numero)

[link interno](#Sottotitolo)

<!-- Commento che non compare nel render markdown -->

| Syntax | Description |
| ------ | ------ |
| Header | Title |
| Paragraph | ![esempio di SVG di svg repo]|


<font color=pink>Testo scritto in rosa</font>

### Sezioni

<details>

<summary>Tips for collapsed sections</summary>

### You can add header

You can add text within a collapsed section.

You can add an image or a code block, too
```ruby
puts "Hello World"
```


</details>

Here is a simple flow chart:

` esempio di mark con i backtick `


<mark>esempio EVIDENZIATO</mark>

<mark style= "background:pink">fhrggrg</mark>

## GRAFICI MERMAID

https://mermaid.js.org/
https://jojozhuang.github.io/tutorial/mermaid-cheat-sheet/

## FLOWCHART BASIC

```mermaid
graph TD;
    A-->B;
    A-->C;
    B-->D;
    C-->D;
```


## GRAFICO FLOWCHART (del gioco)
```mermaid
graph TD;
    A[INDOVINA IL NUMERO] -->|Generatore numeri casuali e dichiarazione variabili,creo un dizionario per memorizzare i tentativi utente|B
    B[Scegli il livello di difficoltà]-->|Utilizzo dello SWITCH|C
    C[LIVELLI]
    C-->D[FACILE]-->G[Numeri da 1-50, 10tentativi, 100 punti]--->L
    D[FACILE]
    C-->E[MEDIO]-->H[Numeri da 1-100, 7tentativi, 200 punti]--->L
    E[MEDIO]
    C-->F[DIFFICILE]
    F[DIFFICILE]-->I[Numeri 1-200, 5tentativi, 300 punti]--->L

    L[ricomincia]
    L-->M-->|Ricomincia,con utilizzo del DO WHILE e della condizione IF|A
    M[SI]
    L-->N-->P
    P[HAI TERMINATO]
    N[NO] ;
```

## FLOWCHART BASIC


```mermaid
graph LR
    A[Square Rect] -- Link text --> B((Circle))
    A --> C(Round Rect)
    B --> D{Rhombus}
    C --> D
```

## FLOWCHART WITH DECISION

```mermaid
graph TD
    A[Christmas] -->|Get money| B(Go shopping)
    B --> C{Let me think}
    C -->|One| D[Laptop]
    C -->|Two| E[iPhone]
    C -->|Three| F[fa:fa-car Car]
```

# FLOWCHART DATA

```mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : places
    CUSTOMER {
        string name
        string custNumber
        string sector
    }
    ORDER ||--|{ LINE-ITEM : contains
    ORDER {
        int orderNumber
        string deliveryAddress
    }
    LINE-ITEM {
        string productCode
        int quantity
        float pricePerUnit
    }
```

## GRAFICI GANTT

```mermaid

gantt
    title A Gantt Diagram
    dateFormat  YYYY-MM-DD
    section Section
    First Task       :a1, 2018-07-01, 30d
    Another Task     :after a1, 20d
    section Another
    Second Task      :2018-07-12, 12d
    Third Task       : 24d

```

```mermaid

gantt
       dateFormat  YYYY-MM-DD
       title Adding GANTT diagram functionality to mermaid

       section A section
       Completed task            :done,    des1, 2018-01-06,2018-01-08
       Active task               :active,  des2, 2018-01-09, 3d
       Future task               :         des3, after des2, 5d
       Future task2              :         des4, after des3, 5d

       section Critical tasks
       Completed task in the critical line :crit, done, 2018-01-06,24h
       Implement parser and jison          :crit, done, after des1, 2d
       Create tests for parser             :crit, active, 3d
       Future task in critical line        :crit, 5d
       Create tests for renderer           :2d
       Add to mermaid                      :1d

       section Documentation
       Describe gantt syntax               :active, a1, after des1, 3d
       Add gantt diagram to demo page      :after a1  , 20h
       Add another diagram to demo page    :doc1, after a1  , 48h

       section Last section
       Describe gantt syntax               :after doc1, 3d
       Add gantt diagram to demo page      :20h
       Add another diagram to demo page    :48h

```

bool pari = somma % 2 == 0;

<summary> HO CREATO UN PULSANTE</summary>

### You can add a header
### You can add an image

## GRAFICI GANTT

```mermaid





