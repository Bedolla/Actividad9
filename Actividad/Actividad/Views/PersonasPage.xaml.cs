using Actividad.Models;
using Actividad.Services;
using System;
using Xamarin.Forms;

namespace Actividad.Views
{
    public partial class PersonasPage : ContentPage
    {
        public PersonasPage() => this.InitializeComponent();

        private void ListaPersonas_OnItemSelected(object transmisor, SelectedItemChangedEventArgs argumentos)
        {
            if (argumentos.SelectedItem is null)
            {
                this.ButtonEditar.IsEnabled = false;
                this.ButtonBorrar.IsEnabled = false;
            }
            else
            {
                this.ButtonEditar.IsEnabled = true;
                this.ButtonBorrar.IsEnabled = true;

                Persona personaSeleccionada = argumentos.SelectedItem as Persona;
                this.EntryId.Text = personaSeleccionada?.Id.ToString();
                this.EntryNombre.Text = personaSeleccionada?.Nombre;
                this.EntryCorreo.Text = personaSeleccionada?.Correo;
                this.EntryTelefono.Text = personaSeleccionada?.Telefono;
            }
        }

        private async void ButtonCrear_OnClicked(object transmisor, EventArgs argumentos)
        {
            if (this.ButtonCrear.Text.Equals("Crear"))
            {
                this.EntryNombre.Text = String.Empty;
                this.EntryCorreo.Text = String.Empty;
                this.EntryTelefono.Text = String.Empty;

                this.EntryNombre.IsReadOnly = false;
                this.EntryCorreo.IsReadOnly = false;
                this.EntryTelefono.IsReadOnly = false;

                this.ButtonCrear.Text = "Finalizar";
                this.ButtonEditar.IsEnabled = false;
                this.ButtonBorrar.IsEnabled = false;

                this.ListViewPersonas.SelectedItem = null;
                this.ListViewPersonas.IsEnabled = false;
            }
            else
            {
                if
                (
                    !String.IsNullOrWhiteSpace(this.EntryNombre.Text) &&
                    !String.IsNullOrWhiteSpace(this.EntryCorreo.Text) &&
                    !String.IsNullOrWhiteSpace(this.EntryTelefono.Text)
                )
                {
                    Persona personaPorCrear = new Persona
                    {
                        Id = Guid.NewGuid(),
                        Nombre = this.EntryNombre.Text.Trim(),
                        Correo = this.EntryCorreo.Text.Trim(),
                        Telefono = this.EntryTelefono.Text.Trim()
                    };

                    if (PersonaService.NoExiste(personaPorCrear))
                    {
                        PersonaService.Crear(personaPorCrear);
                        this.ListViewPersonas.ItemsSource = PersonaService.Listar();
                    }
                    else
                    {
                        await this.DisplayAlert("Ya existe", "Ya hay una persona con ese Nombre o Correo.", "Entendido");
                    }
                }

                this.EntryNombre.Text = String.Empty;
                this.EntryCorreo.Text = String.Empty;
                this.EntryTelefono.Text = String.Empty;

                this.ButtonCrear.Text = "Crear";

                this.EntryNombre.IsReadOnly = true;
                this.EntryCorreo.IsReadOnly = true;
                this.EntryTelefono.IsReadOnly = true;

                this.ListViewPersonas.IsEnabled = true;
            }
        }

        private async void ButtonEditar_OnClicked(object transmisor, EventArgs argumentos)
        {
            if (this.ButtonEditar.Text.Equals("Editar"))
            {
                this.EntryNombre.IsReadOnly = false;
                this.EntryCorreo.IsReadOnly = false;
                this.EntryTelefono.IsReadOnly = false;

                this.ButtonEditar.Text = "Finalizar";
                this.ButtonCrear.IsEnabled = false;
                this.ButtonBorrar.IsEnabled = false;

                this.ListViewPersonas.IsEnabled = false;
            }
            else
            {
                if
                (
                    !String.IsNullOrWhiteSpace(this.EntryNombre.Text) &&
                    !String.IsNullOrWhiteSpace(this.EntryCorreo.Text) &&
                    !String.IsNullOrWhiteSpace(this.EntryTelefono.Text)
                )
                {
                    Persona personaPorEditar = new Persona
                    {
                        Id = new Guid(this.EntryId.Text),
                        Nombre = this.EntryNombre.Text.Trim(),
                        Correo = this.EntryCorreo.Text.Trim(),
                        Telefono = this.EntryTelefono.Text.Trim()
                    };

                    if (PersonaService.NoExiste(personaPorEditar))
                    {
                        PersonaService.Editar(personaPorEditar);
                        this.ListViewPersonas.ItemsSource = PersonaService.Listar();
                    }
                    else
                    {
                        await this.DisplayAlert("Ya existe", "Ya hay una persona con ese Nombre o Correo.", "Entendido");
                    }
                }

                this.EntryNombre.Text = String.Empty;
                this.EntryCorreo.Text = String.Empty;
                this.EntryTelefono.Text = String.Empty;

                this.ListViewPersonas.SelectedItem = null;

                this.ButtonEditar.Text = "Editar";
                this.ButtonCrear.IsEnabled = true;

                this.EntryNombre.IsReadOnly = true;
                this.EntryCorreo.IsReadOnly = true;
                this.EntryTelefono.IsReadOnly = true;

                this.ListViewPersonas.IsEnabled = true;
            }
        }

        private async void ButtonBorrar_OnClicked(object transmisor, EventArgs argumentos)
        {
            if (!await this.DisplayAlert("Borrar", $"¿Realmente desea borrar a {this.EntryNombre.Text}?", "Sí", "No")) return;

            PersonaService.Borrar(this.EntryId.Text);

            this.EntryNombre.Text = String.Empty;
            this.EntryCorreo.Text = String.Empty;
            this.EntryTelefono.Text = String.Empty;

            this.ListViewPersonas.ItemsSource = PersonaService.Listar();
            this.ListViewPersonas.SelectedItem = null;
        }
    }
}
