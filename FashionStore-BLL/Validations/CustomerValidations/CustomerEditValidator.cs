﻿using System;
using System.Linq;
using FashionStore.Entity.Entities;
using FashionStore.Repository.Repositories.Abstracts;
using FluentValidation;

namespace FashionStore_BLL.Validations.CustomerValidations
{
    public class CustomerEditValidator : AbstractValidator<Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerEditValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş bırakılamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.");
            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidDate).WithMessage("Doğum Tarihi alanı boş bırakılamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Lütfen e-posta adresinizi giriniz.")
                .Must(UniqueMailCheck).WithMessage("Bu Email adresi zaten kayıtlı.");
            RuleFor(x => x.Active).NotNull().WithMessage("Durum alanı boş bırakılamaz.");
            RuleFor(x => x.CustomerRoleId).NotEmpty().WithMessage("Yetki alanı boş bırakılamaz.");
        }
        private bool BeAValidDate(DateTime date)
        {
            if (date == default(DateTime))
                return false;
            return true;
        }
        private bool UniqueMailCheck(Customer customer, string email)
        {
            var data = _customerRepository.Where(x => x.Email == email && x.Id != customer.Id && x.Deleted == false).FirstOrDefault();

            if (data == null)
            {
                return true;
            }

            return false;
        }
    }
}
