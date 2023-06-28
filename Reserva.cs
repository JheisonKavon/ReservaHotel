using System;
using Newtonsoft.Json;
using ConsoleTables;

namespace Hotel{
    public class Reserva{

        public int Id {get;private set;}
        public Cliente Cliente{get;private set;}
        public Funcionario Funcionario{get;private set;}
        public int NumQuarto{get;private set;}
        public int QntPessoas{get;private set;}
        public DateTime Check_in{get;private set;}
        public DateTime Check_out{get;private set;}
        public double ValorReserva{get;private set;}

        public Reserva(int id,Cliente cliente,Funcionario funcionario,int numQuarto, int qtdPessoas, DateTime check_in, DateTime check_out,double valorReserva){
            Id = id;
            Cliente = cliente;
            Funcionario = funcionario;
            NumQuarto = numQuarto;
            QntPessoas = qtdPessoas;
            Check_in = check_in;
            Check_out = check_out;
            ValorReserva = valorReserva;
        }

        public static void CriaReserva(Hotel hotel,Funcionario funcionario){
            try{
                Console.Clear();
                Console.WriteLine("Qual tipo de quarto deseja reservar?");
                Console.WriteLine("1 - Quarto padrão");
                Console.WriteLine("2 - Quarto luxo");
                Console.WriteLine("3 - Quarto master");
                Console.WriteLine("0 - Voltar");
                int v = Int32.Parse(hotel.NullString(Console.ReadLine()));
                if(v != 1 && v != 2 && v != 3){
                    return;
                }
                Console.Clear();
                
                Console.WriteLine("Digite o CPF do cliente:");
                string cpf = hotel.NullString(Console.ReadLine());
                if(hotel.clientes.Find(c => c.Cpf == cpf) == null){
                    Console.Clear();
                    Console.WriteLine("Cliente não encontrado");
                    Console.ReadLine();
                    return;
                }
                Console.Clear();
                
                Console.WriteLine("Digite o número do quarto:");
                ListarQuartos(hotel,v);
                int numQUarto = Int32.Parse(hotel.NullString(Console.ReadLine()));
                if(hotel.quartosPadrao.Find(q => q.Numero == numQUarto && q.Disponivel == true) == null && v == 1){
                    Console.Clear();
                    Console.WriteLine("Quarto não encontrado ou indisponível!");
                    Console.ReadLine();
                    return;
                }else if(hotel.quartosLuxo.Find(q => q.Numero == numQUarto && q.Disponivel == true) == null && v == 2){
                    Console.Clear();
                    Console.WriteLine("Quarto não encontrado ou indisponível!");
                    Console.ReadLine();
                    return;
                }else if(hotel.quartosMaster.Find(q => q.Numero == numQUarto && q.Disponivel == true) == null && v == 3){
                    Console.Clear();
                    Console.WriteLine("Quarto não encontrado ou indisponível!");
                    Console.ReadLine();
                    return;
                }
                Console.Clear();
                
                Console.WriteLine("Digite a quantidade de pessoas:");
                int qtdPessoas = Int32.Parse(hotel.NullString(Console.ReadLine()));
                if(hotel.quartosPadrao.Find(q => q.Numero == numQUarto && q.Disponivel == true && qtdPessoas <= q.MaxPessoas) == null && v == 1){
                    Console.Clear();
                    Console.WriteLine("Quantidade de pessoas acima da permitida para esse quarto!");
                    Console.ReadLine();
                    return;
                }else if(hotel.quartosLuxo.Find(q => q.Numero == numQUarto && q.Disponivel == true && qtdPessoas <= q.MaxPessoas) == null && v == 2){
                    Console.Clear();
                    Console.WriteLine("Quantidade de pessoas acima da permitida para esse quarto!");
                    Console.ReadLine();
                    return;
                }else if(hotel.quartosMaster.Find(q => q.Numero == numQUarto && q.Disponivel == true && qtdPessoas <= q.MaxPessoas) == null && v == 3){
                    Console.Clear();
                    Console.WriteLine("Quantidade de pessoas acima da permitida para esse quarto!");
                    Console.ReadLine();
                    return;
                }
                Console.Clear();
                

                Console.WriteLine("Qual ano deseja fazer o checkin?:");
                int ano = Int32.Parse(hotel.NullString(Console.ReadLine()));
                Console.Clear();
                Console.WriteLine("Selecione o mês de checkin:");
                Console.WriteLine("\n1 - Janeiro\n2 - Fevereiro\n3 - Março\n4 - Abril\n5 - Maio\n6 - Junho\n7 - Julho\n8 - Agosto\n9 - Setembro\n10 - Outubro\n11 - Novembro\n12 - Dezembro");
                int mes = Int32.Parse(hotel.NullString(Console.ReadLine()));
                Console.Clear();
                Console.WriteLine("Selecione o dia de checkin:");
                int dia = Int32.Parse(hotel.NullString(Console.ReadLine()));
                DateTime checkin = new DateTime(year: ano, month: mes, day: dia);
                if(DateTime.Compare(checkin,DateTime.Now) <= 0){
                    Console.Clear();
                    Console.WriteLine($"Você precisa reservar para uma data superior a de hoje: {DateTime.Now.ToString("dd/MM/yy")}");
                    Console.ReadLine();
                    return;
                }
                Console.Clear();

                Console.WriteLine("Quantos dias deseja reservar o quarto?:");
                dia = Int32.Parse(hotel.NullString(Console.ReadLine()));
                DateTime checkout = checkin.AddDays(dia);
                if(VerificarReservas(hotel,checkin,checkout,numQUarto) == false){
                    Console.Clear();
                    Console.WriteLine("Essa reserva não pode ser concluida pois já existe uma reserva para esse quarto nesse período");
                    Console.ReadLine();
                    return;
                }

                hotel.ReservarQuarto(cpf,numQUarto,qtdPessoas,checkin,checkout,v,dia,funcionario);
                
                SalvarDadosReserva(hotel);
                Quarto.SalvarDadosQuartos(hotel);
            }catch(Exception e){
                throw new ArgumentException($"Erro ao cadastrar a reserva: {e}");
            }
            
        }
        public static void SalvarDadosReserva(Hotel hotel){
            File.WriteAllText("src/reservas.json",  JsonConvert.SerializeObject(hotel.reservas));
        }
        public static void ListarReservas(Hotel hotel){
            var table = new ConsoleTable("ID","N°Quarto","Cliente","Checkin","Checkout","Total","Funcionário"); 
            Console.Clear(); 
            hotel.reservas.ForEach(obj => {
                table.AddRow(obj.Id, obj.NumQuarto, obj.Cliente.Nome, obj.Check_in.ToString("dd/MM/yy"), obj.Check_out.ToString("dd/MM/yy"), "R$"+obj.ValorReserva,obj.Funcionario.Nome);
            });
            table.Write();
        }
        public static bool VerificarReservas(Hotel hotel,DateTime checkin, DateTime checkout, int numQ){
            List<Reserva> reservasVerificadas = hotel.reservas.FindAll(r => r.NumQuarto == numQ);
            bool retorno = true;
            reservasVerificadas.ForEach(o => {
                if((checkin >= o.Check_in && checkin <= o.Check_out) || (checkout >= o.Check_in && checkout <= o.Check_out)){
                    retorno = false;
                }
            });
            return retorno;
        }
        public static void ListarQuartos(Hotel hotel,int v){
            if(v == 2){
                hotel.quartosLuxo.ForEach(n => {
                    Console.WriteLine($"N° {n.Numero}");
            });
            }else if(v == 1){
                hotel.quartosPadrao.ForEach(n => {
                    Console.WriteLine($"N° {n.Numero}");
            });
            }else if(v == 3){
                hotel.quartosMaster.ForEach(n => {
                    Console.WriteLine($"N° {n.Numero}");
            });
            }
        }
    }
}