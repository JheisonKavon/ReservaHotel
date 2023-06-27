using Newtonsoft.Json;
using ConsoleTables;

namespace Hotel{

    public abstract class Quarto{
        public int Numero{get;protected set;}
        public bool Disponivel{get;protected set;}
        public double ValorDiaria{get;protected set;}
        public int MaxPessoas{get;protected set;}

        public void setDisponivel(bool disp){
            Disponivel = disp;
        }

        public static void ListarQuartos(Hotel hotel){
            var table = new ConsoleTable("N°","Tipo","Disponível"); 
            Console.Clear();
            hotel.quartosPadrao.ForEach(o => {
                table.AddRow(o.Numero, "Padrão", o.Disponivel);
            });
        
            hotel.quartosLuxo.ForEach(o => {
                table.AddRow(o.Numero, "Luxo", o.Disponivel);
            });
        
            hotel.quartosMaster.ForEach(o => {
                table.AddRow(o.Numero, "Master", o.Disponivel);
            });
            table.Write();
            Console.WriteLine("\nSelecione uma opção:");
            Console.WriteLine("1 - Alterar disponibilidade");
            Console.WriteLine("2 - Voltar");
            int v = Int32.Parse(hotel.NullString(Console.ReadLine()));
            if(v == 1){
                Console.WriteLine("Digite o número do quarto");
                int num = Int32.Parse(hotel.NullString(Console.ReadLine()));
                AlterarDisponibilidade(hotel,num);
            }else{
                return;
            }
        }
        public static void AlterarDisponibilidade(Hotel hotel, int num){
            hotel.quartosLuxo.ForEach(n => {
                if(num == n.Numero){
                    n.setDisponivel(!n.Disponivel);
                    Console.Clear();
                    Console.WriteLine("Disponibilidade alterada com sucesso");
                    Console.ReadLine();
                }
            });
            hotel.quartosPadrao.ForEach(n => {
                if(num == n.Numero){
                    n.setDisponivel(!n.Disponivel);
                    Console.Clear();
                    Console.WriteLine("Disponibilidade alterada com sucesso");
                    Console.ReadLine();
                }
            });
            hotel.quartosMaster.ForEach(n => {
                if(num == n.Numero){
                    n.setDisponivel(!n.Disponivel);
                    Console.Clear();
                    Console.WriteLine("Disponibilidade alterada com sucesso");
                    Console.ReadLine();
                }
            });
            SalvarDadosQuartos(hotel);
        }
        public static void SalvarDadosQuartos(Hotel hotel){
            File.WriteAllText("src/quartosLuxo.json", JsonConvert.SerializeObject(hotel.quartosLuxo));
            File.WriteAllText("src/quartosPadrao.json", JsonConvert.SerializeObject(hotel.quartosPadrao));
            File.WriteAllText("src/quartosMaster.json", JsonConvert.SerializeObject(hotel.quartosMaster));
        }
    }
}