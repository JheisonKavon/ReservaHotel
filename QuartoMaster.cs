using System;

namespace Hotel{
    public class QuartoMaster : Quarto{
        public bool Suite{get;private set;}
        public bool Hidro{get;private set;}

        public QuartoMaster(int numero,bool disponivel,double valorDiaria,int maxPessoas,bool suite,bool hidro){
            Numero = numero;
            Disponivel = disponivel;
            ValorDiaria = valorDiaria;
            MaxPessoas = maxPessoas;
            Suite = suite;
            Hidro = hidro;
        }
        public void setSuite(bool suite){
            Suite = suite;
        }
        public void setHidro(bool hidro){
            Hidro = hidro;
        }
    }
}