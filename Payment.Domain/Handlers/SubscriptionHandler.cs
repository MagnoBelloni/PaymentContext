using Flunt.Notifications;
using Flunt.Validations;
using Payment.Domain.Commands;
using Payment.Domain.Entities;
using Payment.Domain.Enums;
using Payment.Domain.Repositories;
using Payment.Domain.Services;
using Payment.Domain.ValueObjects;
using Payment.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Documment", "Este CPF já está em uso");

            //Verificar se E-mail já está cadastrado
            if (_studentRepository.EmailExistis(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);


            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.Number, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.PayerDocumentType, command.Payer, address, email, command.BarCode, command.BoletoNumber);


            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            if(Invalid)
            {
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            //Salvar as informações
            _studentRepository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "bem vindo ao balta.io", "Sua assinatura foi criada");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Documment", "Este CPF já está em uso");

            //Verificar se E-mail já está cadastrado
            if (_studentRepository.EmailExistis(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            //Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);


            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.Number, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.PayerDocumentType, command.Payer, address, email, command.TransactionCode);


            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar as informações
            _studentRepository.CreateSubscription(student);

            //Enviar E-mail de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "bem vindo ao balta.io", "Sua assinatura foi criada");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
