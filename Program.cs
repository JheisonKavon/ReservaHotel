using System;

namespace Hotel{
    class Program{

        static void Main(string[] args){
            
            int opcao;
            Hotel hotel = new Hotel();
            Funcionario funcionarioAtual = Funcionario.ProcuraFuncionario(hotel,"","");

                do{
                    hotel.CarregarDados();

                    while(funcionarioAtual == null){
                        Console.Clear();
                        Console.WriteLine("----| Login |----");
                        Console.WriteLine("Usuário:");
                        string usuario = hotel.NullString(Console.ReadLine());
                        Console.WriteLine("Senha:");
                        string senha = "";
                        ConsoleKeyInfo key;
                        do{
                            key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Backspace){
                                Console.Write("\b");
                            }else if (!char.IsControl(key.KeyChar)){
                                senha += key.KeyChar;
                                Console.Write("*");
                            }
                        }
                        while (key.Key != ConsoleKey.Enter);
                        funcionarioAtual = Funcionario.ProcuraFuncionario(hotel,usuario,senha);
                        if(funcionarioAtual == null){
                            Console.Clear();
                            Console.WriteLine("Usuário ou senha incorreta");
                            Console.WriteLine("1 - Voltar");
                            Console.WriteLine("0 - Sair");
                            int v = Int32.Parse(hotel.NullString(Console.ReadLine()));
                            if(v == 0){
                                Console.Clear();
                                Console.WriteLine("Sistema encerrado!");
                                Environment.Exit(0); 
                            }
                        }
                    }

                    Console.Clear();
                    Console.WriteLine("----| Bem vindo ao Hotel |----");
                    Console.WriteLine("Selecione a opção desejada:");
                    Console.WriteLine("1 - Cadastrar cliente");
                    Console.WriteLine("2 - Consultar cliente");
                    Console.WriteLine("3 - Criar reserva");
                    Console.WriteLine("4 - Consultar reserva");
                    Console.WriteLine("5 - Criar quarto");
                    Console.WriteLine("6 - Listar quartos");
                    Console.WriteLine("0 - Encerrar");

                    int.TryParse(Console.ReadLine(), out opcao);

                    switch(opcao){
                        case 1:
                            Cliente.CadastroCliente(hotel);
                            break;
                        case 2:
                            Cliente.ConsultaPessoas(hotel);
                            break;
                        case 3:
                            Reserva.CriaReserva(hotel,funcionarioAtual);
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
                            Quarto.CriaQuarto(hotel);
                            break;
                        case 6:
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