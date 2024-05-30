using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using DesafioFundamentos.Controllers;
using DesafioFundamentos.Models;

namespace EstacionamentoControllerTests
{
    [TestFixture]
    public class EstacionamentoControllerTests
    {
        private EstacionamentoController _controller;
        private Mock<TextReader> _mockReader;
        private Mock<TextWriter> _mockWriter;

        [SetUp]
        public void Setup()
        {
            _controller = new EstacionamentoController(10, 5.0, 2.0);
            _mockReader = new Mock<TextReader>();
            _mockWriter = new Mock<TextWriter>();
            Console.SetIn(_mockReader.Object);
            Console.SetOut(_mockWriter.Object);
        }

        [Test]
        public void ExibirMenuPrincipal_DisplaysMenu()
        {
            _controller.ExibirMenuPrincipal();
            _mockWriter.Verify(w => w.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Test]
        public void AdicionarVeiculo_AddsVehicle()
        {
            _mockReader.SetupSequence(r => r.ReadLine())
                       .Returns("ABC1234"); // Simulating user input

            _controller.AdicionarVeiculo();

            Assert.IsTrue(_controller._estacionamento.veiculos.Exists(v => v.PlacaDoCarroEstacionado == "ABC1234"));
        }

        [Test]
        public void RemoverVeiculo_RemovesVehicle()
        {
            _mockReader.SetupSequence(r => r.ReadLine())
                       .Returns("ABC1234")
                       .Returns("1");

            _controller.AdicionarVeiculo();
            _controller.RemoverVeiculo();

            Assert.IsFalse(_controller._estacionamento.veiculos.Exists(v => v.PlacaDoCarroEstacionado == "ABC1234"));
        }

        [Test]
        public void ListarVeiculos_DisplaysVehicles()
        {
            _mockReader.Setup(r => r.ReadLine()).Returns("0");
            _controller.AdicionarVeiculo();
            _controller.ListarVeiculos();
            _mockWriter.Verify(w => w.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
