using ConsultarCEP.Servico;
using ConsultarCEP.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
           //TODO - validacoes
            string cep = CEP.Text.Trim(); //Prop da tela que armazena dados digitado do usuario. Trim(retina spacs brancos)
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                        RESULTADO.Text = string.Format("Endereço: {2}, Bairro: {3}, Localização: {0}-{1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    else
                        DisplayAlert("ERRO", "Não foi encontrado um endereço para o CEP informado: " + cep, "OK");
                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valid = true;
            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres", "OK");
                valid = false;
            }
            int novoCEP;
            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter apenas números", "OK");
                valid = false;
            }
            return valid;
        }
    }
}
