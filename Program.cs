using System;

namespace UnicamCompleanno
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Questo programma ti dice la tua eta' e il numero di giorni che restano al tuo compleanno");
            Console.WriteLine("Digita la tua data di nascita: ");
            string testoDigitatoDallUtente = Console.ReadLine();

            try {
                DateTime dataDiNascita = DateTime.Parse(testoDigitatoDallUtente);
                bool dataDiNascitaèBisestile = DateTime.IsLeapYear(dataDiNascita.Year);
                //Calcoliamo gli anni dell'utente.
                //Quando non sappiamo come si fa o quando, come in questo caso,
                //.NET non ha un modo facile per ottenere la differenza in anni
                //andiamo a vedere su StackOverlow se qualcuno ha già avuto la stessa necessità
                //Il metodo Years è stato preso da lì (è definito più in basso, in questo file)
                int anniDiDifferenza = Years(dataDiNascita, DateTime.Today);
                Console.WriteLine($"Oggi hai {anniDiDifferenza} anni");

                //Ora calcoliamo i giorni che ci separano dal prossimo compleanno
                DateTime dataProssimoCompleanno;
                bool prossimoCompleannoQuestAnno=false;
                bool natoIlVentinoveFebbraio=false;
                // la differenza deve tener conto della data attuale, altrimenti restituirà un numero negativo
                if(dataDiNascita.Day> DateTime.Today.Day && dataDiNascita.Month>DateTime.Today.Month){
                    prossimoCompleannoQuestAnno=true;
                }
                // controllo anche il caso particolare in cui l'utente è nato il 29 febbraio
                if(dataDiNascita.Month==2 && dataDiNascita.Day==29){
                    natoIlVentinoveFebbraio=true;
                }
                int annoProssimoCompleanno;
                if(prossimoCompleannoQuestAnno)
                    annoProssimoCompleanno = DateTime.Today.Year;
                else annoProssimoCompleanno = DateTime.Today.Year+1;
                bool prossimoCompleannoèBisestile = DateTime.IsLeapYear(annoProssimoCompleanno);
                // a questo punto controllo: se l'anno del prossimo compleanno non è bisestile
                // e l'utente è nato il 29 febbraio è assegnata come data di compleanno il primo marzo
                if((!prossimoCompleannoèBisestile) && natoIlVentinoveFebbraio)
                dataProssimoCompleanno = new DateTime(annoProssimoCompleanno, 3, 1);
                // altrimenti non devo preoccuparmi di nulla
                else dataProssimoCompleanno= new DateTime(annoProssimoCompleanno, dataDiNascita.Month, dataDiNascita.Day);

                TimeSpan differenzaTemporale = dataProssimoCompleanno - DateTime.Today;
                int differenzaInGiorni = int.Parse(differenzaTemporale.TotalDays.ToString());
                if (differenzaInGiorni!=365)
                Console.WriteLine($"Restano {differenzaInGiorni} giorni al tuo prossimo compleanno!");
                else
                Console.WriteLine($"Auguri!");

            } catch (Exception exc) {
                Console.WriteLine($"Non hai digitato una data valida, il programma ora terminera'. L'errore e' stato: {exc.Message}.");
            }
            Console.ReadKey();
        }

        //Grazie, StackOverflow!
        //http://stackoverflow.com/questions/4127363/date-difference-in-years-using-c-sharp#answer-4127477
        static int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }
}
