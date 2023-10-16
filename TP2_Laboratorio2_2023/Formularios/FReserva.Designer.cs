namespace TP2_Laboratorio2_2023.Formularios
{
    partial class FReserva
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbReserva = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPropiedad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateDesde = new System.Windows.Forms.DateTimePicker();
            this.dateHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbClientes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbReserva
            // 
            this.lbReserva.AutoSize = true;
            this.lbReserva.Location = new System.Drawing.Point(111, 9);
            this.lbReserva.Name = "lbReserva";
            this.lbReserva.Size = new System.Drawing.Size(77, 13);
            this.lbReserva.TabIndex = 0;
            this.lbReserva.Text = "Nueva reserva";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cliente:";
            // 
            // tbPropiedad
            // 
            this.tbPropiedad.Location = new System.Drawing.Point(76, 69);
            this.tbPropiedad.Name = "tbPropiedad";
            this.tbPropiedad.Size = new System.Drawing.Size(100, 20);
            this.tbPropiedad.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Propiedad:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Desde:";
            // 
            // dateDesde
            // 
            this.dateDesde.Location = new System.Drawing.Point(76, 102);
            this.dateDesde.Name = "dateDesde";
            this.dateDesde.Size = new System.Drawing.Size(200, 20);
            this.dateDesde.TabIndex = 6;
            // 
            // dateHasta
            // 
            this.dateHasta.Location = new System.Drawing.Point(76, 131);
            this.dateHasta.Name = "dateHasta";
            this.dateHasta.Size = new System.Drawing.Size(200, 20);
            this.dateHasta.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Hasta:";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(62, 194);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(143, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbClientes
            // 
            this.cbClientes.FormattingEnabled = true;
            this.cbClientes.Location = new System.Drawing.Point(76, 37);
            this.cbClientes.Name = "cbClientes";
            this.cbClientes.Size = new System.Drawing.Size(121, 21);
            this.cbClientes.TabIndex = 11;
            // 
            // FReserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 228);
            this.Controls.Add(this.cbClientes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dateHasta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateDesde);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPropiedad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbReserva);
            this.Name = "FReserva";
            this.Text = "Agregar reserva";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbReserva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox tbPropiedad;
        public System.Windows.Forms.DateTimePicker dateDesde;
        public System.Windows.Forms.DateTimePicker dateHasta;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.ComboBox cbClientes;
    }
}