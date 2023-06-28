using System;
using ConsoleTables;

namespace Hotel{

    public abstract class Pessoa: IPessoa{
        public string Cpf{get;protected set;}
        public string Nome{get;protected set;}
        public int Id{get;protected set;}
        public string Telefone{get;protected set;}

        

        public static void ConsultaPessoas(Hotel hotel){
            var table = new ConsoleTable("Id","Nome"); 
            Console.Clear();
            hotel.clientes.ForEach(obj => {
                table.AddRow(obj.Id, obj.Nome);
            });
            hotel.funcionarios.ForEach(obj => {
                table.AddRow(obj.Id, obj.Nome);
            });
            table.Write();
            Console.ReadLine();
        }
    }
}