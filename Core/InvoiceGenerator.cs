using Ninance_v2.Core;
using s2industries.ZUGFeRD;
using System;
using System.Collections.Generic;
using System.IO;
using QuestPDF.Helpers;
using System.Linq;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Spire.Pdf.Conversion;

namespace Ninance_v2.Core
{
    public class InvoiceGenerator
    {
        public class InvoiceModel
        {
            public string InvoiceNumber { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime DueDate { get; set; }

            public Address SellerAddress { get; set; }
            public Address CustomerAddress { get; set; }

            public List<OrderItem> Items { get; set; }
            public string Comments { get; set; }
        }

        public class OrderItem
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        public class Address
        {
            public string PersonName { get; set; }
            public string CompanyName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Postcode { get; set; }
            public string CountryName { get; set; }
            public string CountryCode { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public static class InvoiceDocumentDataSource
        {
            private static Random Random = new Random();

            public static InvoiceModel GetInvoiceDetails()
            {
                var items = Enumerable
                    .Range(1, 8)
                    .Select(i => GenerateRandomOrderItem())
                    .ToList();

                return new InvoiceModel
                {
                    InvoiceNumber = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 10),
                    IssueDate = DateTime.Now,
                    DueDate = DateTime.Now + TimeSpan.FromDays(14),

                    SellerAddress = GenerateRandomAddress(),
                    CustomerAddress = GenerateRandomAddress(),

                    Items = items,
                    Comments = Placeholders.Paragraph()
                };
            }

            private static OrderItem GenerateRandomOrderItem()
            {
                return new OrderItem
                {
                    Name = Placeholders.Label(),
                    Price = (decimal)Math.Round(Random.NextDouble() * 100, 2),
                    Quantity = Random.Next(1, 10)
                };
            }

            private static Address GenerateRandomAddress()
            {
                return new Address
                {
                    PersonName = Placeholders.Name(),
                    CompanyName = Placeholders.Name(),
                    Street = Placeholders.Label(),
                    City = Placeholders.Label(),
                    State = Placeholders.Label(),
                    CountryCode = "DE",
                    CountryName = Placeholders.Label(),
                    Postcode = Placeholders.Label(),
                    Email = Placeholders.Email(),
                    Phone = Placeholders.PhoneNumber()
                };
            }
        }

        public class AddressComponent : IComponent
        {
            private string Title { get; }
            private Address Address { get; }

            public AddressComponent(string title, Address address)
            {
                Title = title;
                Address = address;
            }

            public void Compose(IContainer container)
            {
                container.Stack(stack =>
                {
                    stack.Spacing(2);

                    stack.Item().BorderBottom(1).PaddingBottom(5).Text(Title, TextStyle.Default.SemiBold());

                    stack.Item().Text(Address.PersonName);
                    stack.Item().Text(Address.Street);
                    stack.Item().Text($"{Address.City}, {Address.State}");
                    stack.Item().Text(Address.Email);
                    stack.Item().Text(Address.Phone);
                });
            }
        }

        public class InvoiceDocument : IDocument
        {
            public InvoiceModel Model { get; }

            public InvoiceDocument(InvoiceModel model)
            {
                Model = model;
            }

            public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

            public void Compose(IDocumentContainer container)
            {
                container
                    .Page(page =>
                    {
                        page.Margin(50);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);


                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                    });
            }

            void ComposeHeader(IContainer container)
            {
                var titleStyle = TextStyle.Default.Size(20).SemiBold().Color(Colors.Blue.Medium);

                container.Row(row =>
                {
                    row.RelativeColumn().Stack(stack =>
                    {
                        stack.Item().Text($"Invoice #{Model.InvoiceNumber}", titleStyle);

                        stack.Item().Text(text =>
                        {
                            text.Span("Issue date: ", TextStyle.Default.SemiBold());
                            text.Span($"{Model.IssueDate:d}");
                        });

                        stack.Item().Text(text =>
                        {
                            text.Span("Due date: ", TextStyle.Default.SemiBold());
                            text.Span($"{Model.DueDate:d}");
                        });
                    });

                    row.ConstantColumn(100).Height(50).Placeholder();
                });
            }

            void ComposeContent(IContainer container)
            {
                container.PaddingVertical(40).Stack(stack =>
                {
                    stack.Spacing(5);

                    stack.Item().Row(row =>
                    {
                        row.RelativeColumn().Component(new AddressComponent("", Model.SellerAddress));
                        row.RelativeColumn().Component(new AddressComponent("From", Model.SellerAddress));
                        row.ConstantColumn(50);
                        row.RelativeColumn().Component(new AddressComponent("For", Model.CustomerAddress));
                    });

                    stack.Item().Element(ComposeTable);

                    var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
                    stack.Item().AlignRight().Text($"Grand total: {totalPrice}€", TextStyle.Default.Size(14));

                    if (!string.IsNullOrWhiteSpace(Model.Comments))
                        stack.Item().PaddingTop(25).Element(ComposeComments);
                });
            }

            void ComposeTable(IContainer container)
            {
                container.PaddingTop(10).Decoration(decoration =>
                {
                    // header
                    decoration.Header().BorderBottom(1).Padding(5).Row(row =>
                    {
                        row.ConstantColumn(25).Text("#");
                        row.RelativeColumn(3).Text("Product");
                        row.RelativeColumn().AlignRight().Text("Unit price");
                        row.RelativeColumn().AlignRight().Text("Quantity");
                        row.RelativeColumn().AlignRight().Text("Total");
                    });

                    // content
                    decoration
                        .Content()
                        .Stack(stack =>
                        {
                            foreach (var item in Model.Items)
                            {
                                stack.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Row(row =>
                                {
                                    row.ConstantColumn(25).Text(Model.Items.IndexOf(item) + 1);
                                    row.RelativeColumn(3).Text(item.Name);
                                    row.RelativeColumn().AlignRight().Text($"{item.Price}€");
                                    row.RelativeColumn().AlignRight().Text(item.Quantity);
                                    row.RelativeColumn().AlignRight().Text($"{item.Price * item.Quantity}€");
                                });
                            }
                        });
                });
            }

            void ComposeComments(IContainer container)
            {
                container.Background(Colors.Grey.Lighten3).Padding(10).Stack(stack =>
                {
                    stack.Spacing(5);
                    stack.Item().Text("Comments", TextStyle.Default.Size(14));
                    stack.Item().Text(Model.Comments);
                });
            }
        }

        public static void GeneratePdfInvoice(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            var zugferdPath = Path.GetDirectoryName(path) + "/ZUGFeRD-invoice.xml";

            if (File.Exists(zugferdPath))
                File.Delete(zugferdPath);

            var zugferdStream = new MemoryStream();

            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            var document = new InvoiceDocument(model);
            document.GeneratePdf(path);

            var invoiceZugferd = InvoiceDescriptor.CreateInvoice(model.InvoiceNumber, model.IssueDate, CurrencyCodes.EUR);
            invoiceZugferd.GrandTotalAmount = model.Items.Sum(x => x.Price * x.Quantity);
            invoiceZugferd.AllowanceTotalAmount = new decimal(0.00);
            invoiceZugferd.LineTotalAmount = new decimal(0.00);
            invoiceZugferd.ChargeTotalAmount = new decimal(0.00);
            invoiceZugferd.TaxBasisAmount = new decimal(0.00);
            invoiceZugferd.TaxTotalAmount = new decimal(0.00);
            invoiceZugferd.AddTradeLineItem("Test", "Testing", QuantityCodes.PCE);
            invoiceZugferd.Buyer = new Party() { Country = (CountryCodes)Enum.Parse(typeof(CountryCodes), model.CustomerAddress.CountryCode, true), City = model.CustomerAddress.City, Street = model.CustomerAddress.City, Name = model.CustomerAddress.CompanyName, Postcode = model.CustomerAddress.Postcode, ContactName = model.CustomerAddress.PersonName };
            invoiceZugferd.Seller = new Party() { Country = (CountryCodes)Enum.Parse(typeof(CountryCodes), model.SellerAddress.CountryCode, true), City = model.SellerAddress.City, Street = model.SellerAddress.City, Name = model.SellerAddress.CompanyName, Postcode = model.SellerAddress.Postcode, ContactName = model.CustomerAddress.PersonName };
            invoiceZugferd.Save(zugferdStream, ZUGFeRDVersion.Version1, Profile.Basic);

            PdfDocument pdfDocument = PdfReader.Open(path);
            pdfDocument.AddEmbeddedFile("ZUGFeRD-invoice.xml", zugferdStream);
            pdfDocument.Info.CreationDate = DateTime.Now;
            pdfDocument.Info.Author = "Variaty Studios";
            pdfDocument.Save(path);
            pdfDocument.Close();

            PdfStandardsConverter pdfConverter = new PdfStandardsConverter(path);
            pdfConverter.ToPdfA3A(path);

            zugferdStream.Close();
        }
    }
}
