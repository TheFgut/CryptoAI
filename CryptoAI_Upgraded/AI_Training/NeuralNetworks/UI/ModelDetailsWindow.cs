using Keras.Models;
using Numpy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoAI_Upgraded.AI_Training.NeuralNetworks.UI
{
    public partial class ModelDetailsWindow : Form
    {
        public ModelDetailsWindow(NeuralNetwork? neuralNetwork)
        {
            InitializeComponent();
            ShowInfo(neuralNetwork);
        }

        private void ShowInfo(NeuralNetwork? neuralNetwork)
        {
            DetailsTextBox.Text = "";
            if (neuralNetwork == null)
            {
                DetailsTextBox.Text = "No network to analize";
                return;
            }
            DetailsTextBox.Text = GetModelWeights(neuralNetwork.model);
        }

        public string GetModelWeights(BaseModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Модель содержит следующие слои и веса:\n");

            List<NDarray> weights = model.GetWeights();
            for (int i = 0; i < weights.Count; i++)
            {
                sb.AppendLine($"  - Вес #{i + 1}: {weights[i].ToString()}");
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}
