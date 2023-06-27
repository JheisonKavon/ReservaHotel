using Newtonsoft.Json;

namespace Hotel{

    public abstract class Quarto{
        public int Numero{get;protected set;}
        public bool Disponivel{get;protected set;}
        public double ValorDiaria{get;protected set;}
        public int MaxPessoas{get;protected set;}

        public void setDisponivel(bool disp){
            Disponivel = disp;
        }

        public static void ListarQuartos(Hotel hotel,int v){
            if(v == 1){
                hotel.quartosPadrao.ForEach(o => {
                    Console.WriteLine($"N°: {o.Numero}");
                });
            }else if(v == 2){
                hotel.quartosLuxo.ForEach(o => {
                    Console.WriteLine($"N°: {o.Numero}");
                });
            }else if(v == 3){
                hotel.quartosMaster.ForEach(o => {
                    Console.WriteLine($"N°: {o.Numero}");
                });
            }
        }

        public static void SalvarDadosQuartos(Hotel hotel){
            File.WriteAllText("src/quartosLuxo.json", JsonConvert.SerializeObject(hotel.quartosLuxo));
            File.WriteAllText("src/quartosPadrao.json", JsonConvert.SerializeObject(hotel.quartosPadrao));
            File.WriteAllText("src/quartosMaster.json", JsonConvert.SerializeObject(hotel.quartosMaster));
        }
    }

}