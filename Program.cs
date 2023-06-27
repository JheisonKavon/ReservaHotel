using System;

namespace Hotel{
    class Program{

        static void Main(string[] args){
            
            int opcao;
            Hotel hotel = new Hotel();

                do{
                    hotel.CarregarDados();
                    Console.Clear();
                    Console.WriteLine("----| Bem vindo ao Hotel |----");
                    Console.WriteLine("Selecione a opção desejada:");
                    Console.WriteLine("1 - Cadastrar cliente");
                    Console.WriteLine("2 - Consultar cliente");
                    Console.WriteLine("3 - Criar reserva");
                    Console.WriteLine("4 - Consultar reserva");
                    Console.WriteLine("5 - Listar Quartos");
                    Console.WriteLine("0 - Encerrar");

                    int.TryParse(Console.ReadLine(), out opcao);

                    switch(opcao){
                        case 1:
                            Cliente.CadastroCliente(hotel);
                            break;
                        case 2:
                            Cliente.ConsultaClientes(hotel);
                            break;
                        case 3:
                            Reserva.CriaReserva(hotel);
                            break;
                        case 4:
                            int opcaoReserva;
                            Reserva.ListarReservas(hotel);
                            Console.WriteLine("\nSelecione uma opção:");
                            Console.WriteLine("1 - Excluir reserva");
                            Console.WriteLine("2 - Voltar");
                            int.TryParse(Console.ReadLine(), out opcaoReserva);
                                switch(opcaoReserva){
                                    case 1:
                                        Console.WriteLine("\nDigite o ID da reserva para excluir...");
                                        int idExcluir = Int32.Parse(hotel.NullString(Console.ReadLine()));
                                        hotel.CancelarReservaQuarto(idExcluir,hotel);
                                        Reserva.ListarReservas(hotel);
                                        break;
                                    case 2:
                                        break;
                                }
                            break;
                        case 5:
                            Quarto.ListarQuartos(hotel);
                            break;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("Sistema encerrado!");
                            Environment.Exit(0);                                                       
                            break;
                    }
                }while(opcao!=0);
                
        }
    }
}