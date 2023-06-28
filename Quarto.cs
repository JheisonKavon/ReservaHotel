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
            var table = new ConsoleTable("N°","Tipo","Disponível","Suite","Hidro","Valor diária"); 
            Console.Clear();
            hotel.quartosPadrao.ForEach(o => {
                table.AddRow(o.Numero, "Padrão", o.Disponivel,"","","R$"+o.ValorDiaria);
            });
        
            hotel.quartosLuxo.ForEach(o => {
                table.AddRow(o.Numero, "Luxo", o.Disponivel,o.Suite,"","R$"+o.ValorDiaria);
            });
        
            hotel.quartosMaster.ForEach(o => {
                table.AddRow(o.Numero, "Master", o.Disponivel,o.Suite,o.Hidro,"R$"+o.ValorDiaria);
            });
            table.Write();
            ModificarQuartos(hotel);
        }
        public static void ModificarQuartos(Hotel hotel){
            Console.WriteLine("\nSelecione uma opção:");
            Console.WriteLine("1 - Alterar disponibilidade");
            Console.WriteLine("2 - Alterar suite");
            Console.WriteLine("3 - Alterar hidro");
            Console.WriteLine("4 - Alterar valor diária");
            Console.WriteLine("0 - Voltar");
            int v = Int32.Parse(hotel.NullString(Console.ReadLine()));
            if(v == 1){
                Console.WriteLine("\nDigite o número do quarto");
                int num = Int32.Parse(hotel.NullString(Console.ReadLine()));
                AlterarDisponibilidade(hotel,num);
            }else if(v == 2){
                Console.WriteLine("\nDigite o número de um quarto de luxo ou master");
                int num = Int32.Parse(hotel.NullString(Console.ReadLine()));
                AlterarSuite(hotel,num);
            }else if(v == 3){
                Console.WriteLine("\nDigite o número de um quarto master");
                int num = Int32.Parse(hotel.NullString(Console.ReadLine()));
                ALterarHidro(hotel,num);
            }else if(v == 4){
                Console.WriteLine("\nDigite o número do quarto");
                int num = Int32.Parse(hotel.NullString(Console.ReadLine()));
                Console.WriteLine("\nDigite o novo valor da diária");
                double valor = Double.Parse(hotel.NullString(Console.ReadLine()));
                AlterarValorDiaria(hotel,num,valor);
            }
            else{
                return;
            }
        }
        public static void AlterarValorDiaria(Hotel hotel,int num,double valor){
            bool achou = false;
            hotel.quartosLuxo.ForEach(n => {
                if(num == n.Numero){
                    n.ValorDiaria = valor;
                    Console.Clear();
                    Console.WriteLine("Diária alterada com sucesso");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            hotel.quartosPadrao.ForEach(n => {
                if(num == n.Numero){
                    n.ValorDiaria = valor;
                    Console.Clear();
                    Console.WriteLine("Diária alterada com sucesso");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            hotel.quartosMaster.ForEach(n => {
                if(num == n.Numero){
                    n.ValorDiaria = valor;
                    Console.Clear();
                    Console.WriteLine("Diária alterada com sucesso");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            if(achou)
                return;
            Console.Clear();
            Console.WriteLine("Número de quarto inválido");
            Console.ReadLine();
        }
        public static void ALterarHidro(Hotel hotel,int num){
            bool achou = false;
            hotel.quartosMaster.ForEach(n => {
                if(num == n.Numero){
                    n.setHidro(!n.Hidro);
                    Console.Clear();
                    Console.WriteLine("Hidro alterada!");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            if(achou)
                return;
            Console.Clear();
            Console.WriteLine("Número de quarto inválido");
            Console.ReadLine();
        }
        public static void AlterarSuite(Hotel hotel, int num){
            bool achou = false;
            hotel.quartosLuxo.ForEach(n => {
                if(num == n.Numero){
                    n.setSuite(!n.Suite);
                    Console.Clear();
                    Console.WriteLine("Suite alterada!");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            hotel.quartosMaster.ForEach(n => {
                if(num == n.Numero){
                    n.setSuite(!n.Suite);
                    Console.Clear();
                    Console.WriteLine("Suite alterada!");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            if(achou)
                return;
            Console.Clear();
            Console.WriteLine("Número de quarto inválido");
            Console.ReadLine();
        }
        public static void AlterarDisponibilidade(Hotel hotel, int num){
            bool achou = false;
            hotel.quartosLuxo.ForEach(n => {
                if(num == n.Numero){
                    n.setDisponivel(!n.Disponivel);
                    Console.Clear();
                    Console.WriteLine("Disponibilidade alterada com sucesso");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            hotel.quartosPadrao.ForEach(n => {
                if(num == n.Numero){
                    n.setDisponivel(!n.Disponivel);
                    Console.Clear();
                    Console.WriteLine("Disponibilidade alterada com sucesso");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            hotel.quartosMaster.ForEach(n => {
                if(num == n.Numero){
                    n.setDisponivel(!n.Disponivel);
                    Console.Clear();
                    Console.WriteLine("Disponibilidade alterada com sucesso");
                    Console.ReadLine();
                    SalvarDadosQuartos(hotel);
                    achou = true;
                }
            });
            if(achou)
                return;
            Console.Clear();
            Console.WriteLine("Número de quarto inválido");
            Console.ReadLine();
        }
        public static void CriaQuarto(Hotel hotel){
            try{   
                Console.Clear();
                Console.WriteLine("Qual novo quarto será criado?");
                Console.WriteLine("1 - Quarto padrão");
                Console.WriteLine("2 - Quarto luxo");
                Console.WriteLine("3 - Quarto master");
                Console.WriteLine("0 - Voltar");
                int v = Int32.Parse(hotel.NullString(Console.ReadLine()));
                if(v != 1 && v != 2 && v != 3){
                    return;
                }
                Console.Clear();

                if(v == 1){
                    QuartoPadrao quartoPadrao = new QuartoPadrao(hotel.quartosPadrao.Last().Numero + 1, true, 100.0, 2);
                    hotel.quartosPadrao.Add(quartoPadrao);
                    SalvarDadosQuartos(hotel);
                }else if(v == 2){
                    QuartoLuxo quartoLuxo = new QuartoLuxo(hotel.quartosLuxo.Last().Numero + 1, true, 300.0, 3,true);
                    hotel.quartosLuxo.Add(quartoLuxo);
                    SalvarDadosQuartos(hotel);
                }else if(v == 3){
                    QuartoMaster quartoMaster = new QuartoMaster(hotel.quartosMaster.Last().Numero + 1, true, 500.0, 5, true, true);
                    hotel.quartosMaster.Add(quartoMaster);
                    SalvarDadosQuartos(hotel);
                }
                Console.Clear();
                Console.WriteLine("Novo quarto adicionado!");
                Console.ReadLine();
            }catch(Exception e){
                throw new ArgumentException($"Erro ao cadastrar quarto: {e}");
            }
        }
        public static void SalvarDadosQuartos(Hotel hotel){
            File.WriteAllText("src/quartosLuxo.json", JsonConvert.SerializeObject(hotel.quartosLuxo));
            File.WriteAllText("src/quartosPadrao.json", JsonConvert.SerializeObject(hotel.quartosPadrao));
            File.WriteAllText("src/quartosMaster.json", JsonConvert.SerializeObject(hotel.quartosMaster));
        }
    }
}