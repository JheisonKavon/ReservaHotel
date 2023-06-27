using System;
using Newtonsoft.Json;

namespace Hotel{

    public class Hotel{
        public List<Cliente> clientes = new List<Cliente>();
        public List<QuartoPadrao> quartosPadrao = new List<QuartoPadrao>();
        public List<QuartoLuxo> quartosLuxo = new List<QuartoLuxo>();
        public List<QuartoMaster> quartosMaster = new List<QuartoMaster>();
        public List<Reserva> reservas = new List<Reserva>();

        public void ReservarQuarto(string cpfCliente, int numQuarto, int qtdPessoas, DateTime checkin, DateTime checkout,int v){
            try{
                Cliente cliente = clientes.Find(c => c.Cpf == cpfCliente);

                if(cliente == null){
                    Console.Clear();
                    Console.WriteLine("Cliente não encontrado!");
                    Console.ReadLine();
                    return;
                }
                if(v == 1){
                    QuartoPadrao quartoPadrao = quartosPadrao.Find(q => q.Numero == numQuarto && q.Disponivel == true);
                    if(quartoPadrao == null){
                        Console.Clear();
                        Console.WriteLine("Quarto não encontrado ou indisponível!");
                        Console.ReadLine();
                        return;
                    }
                    if(qtdPessoas > quartoPadrao.MaxPessoas){
                        Console.Clear();
                        Console.WriteLine("Quantidade de pessoas acima da permitida para esse quarto!");
                        Console.ReadLine();
                        return;
                    }
                    Reserva reserva = new Reserva(reservas.Last().Id + 1,cliente,quartoPadrao.Numero,qtdPessoas,checkin,checkout);
                    quartoPadrao.setDisponivel(false);
                    reservas.Add(reserva);
                }else if(v == 2){
                    QuartoLuxo quartoLuxo = quartosLuxo.Find(q => q.Numero == numQuarto && q.Disponivel == true);
                    if(quartoLuxo == null){
                        Console.Clear();
                        Console.WriteLine("Quarto não encontrado ou indisponível!");
                        Console.ReadLine();
                        return;
                    }
                    if(qtdPessoas > quartoLuxo.MaxPessoas){
                        Console.Clear();
                        Console.WriteLine("Quantidade de pessoas acima da permitida para esse quarto!");
                        Console.ReadLine();
                        return;
                    }
                    Reserva reserva = new Reserva(reservas.Last().Id + 1,cliente,quartoLuxo.Numero,qtdPessoas,checkin,checkout);
                    quartoLuxo.setDisponivel(false);
                    reservas.Add(reserva);
                }else if(v == 3){
                    QuartoMaster quartoMaster = quartosMaster.Find(q => q.Numero == numQuarto && q.Disponivel == true);
                    if(quartoMaster == null){
                        Console.Clear();
                        Console.WriteLine("Quarto não encontrado ou indisponível!");
                        Console.ReadLine();
                        return;
                    }
                    if(qtdPessoas > quartoMaster.MaxPessoas){
                        Console.Clear();
                        Console.WriteLine("Quantidade de pessoas acima da permitida para esse quarto!");
                        Console.ReadLine();
                        return;
                    }
                    Reserva reserva = new Reserva(reservas.Last().Id + 1,cliente,quartoMaster.Numero,qtdPessoas,checkin,checkout);
                    quartoMaster.setDisponivel(false);
                    reservas.Add(reserva);
                }
                
                Console.Clear();
                Console.WriteLine("Reserva criada com sucesso!");
                Console.ReadLine();

            }catch(Exception e){
                throw new ArgumentException($"Erro ao cadastrar a reserva: {e}");
            }
        }
        public void CancelarReservaQuarto(int verificador,Hotel hotel){
            Reserva x = reservas.Find(q => q.Id == verificador);
            if(x == null){
                Console.WriteLine("Reserva não encontrada");
                Console.ReadLine();
                return;
            } 
            reservas.Remove(x);
            quartosLuxo.ForEach(n => {
                if(x.NumQuarto == n.Numero){
                    n.setDisponivel(true);
                }
            });
            quartosPadrao.ForEach(n => {
                if(x.NumQuarto == n.Numero){
                    n.setDisponivel(true);
                }
            });
            quartosMaster.ForEach(n => {
                if(x.NumQuarto == n.Numero){
                    n.setDisponivel(true);
                }
            });
                
            Reserva.SalvarDadosReserva(hotel);
            QuartoLuxo.SalvarDadosQuartos(hotel);
            Console.Clear();
            Console.WriteLine("Reserva excluída com sucesso!");
            Console.ReadLine();
        }
        public void CarregarDados(){
            if(File.Exists("src/clientes.json")){
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(File.ReadAllText("src/clientes.json"));
            }
            if(File.Exists("src/quartosLuxo.json")){
                quartosLuxo = JsonConvert.DeserializeObject<List<QuartoLuxo>>(File.ReadAllText("src/quartosLuxo.json"));
            }
            if(File.Exists("src/quartosPadrao.json")){
                quartosPadrao = JsonConvert.DeserializeObject<List<QuartoPadrao>>(File.ReadAllText("src/quartosPadrao.json"));
            }
            if(File.Exists("src/quartosMaster.json")){
                quartosMaster = JsonConvert.DeserializeObject<List<QuartoMaster>>(File.ReadAllText("src/quartosMaster.json"));
            }
            if(File.Exists("src/reservas.json")){
                reservas = JsonConvert.DeserializeObject<List<Reserva>>(File.ReadAllText("src/reservas.json"));
            }
        }
        public string NullString(string? s){
            if(s == null){
                throw new ArgumentNullException(paramName: nameof(s), message: "Esse campo não pode ser nulo");
            }else if(s == ""){
                throw new ArgumentNullException(paramName: nameof(s), message: "Esse campo não pode ser nulo");
            }
            else{
                return s;
            }
        }
    }

}