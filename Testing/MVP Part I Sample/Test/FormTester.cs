using System;
using System.Collections.Generic;
using System.Text;
using Common;
using MVP;
using NMock2;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class FormTester
    {
        private Mockery _mockery;
        private IEmployeeView _view;
        private EmployeePresenter _presenter;
        private EmployeeModel _model;
        private IEmployeeService _service;

        [SetUp]
        public void Initialize()
        {
            _mockery = new Mockery();
            _view = _mockery.NewMock<IEmployeeView>();
            _service = _mockery.NewMock<IEmployeeService>();
            _model = new EmployeeModel();
            _presenter = new EmployeePresenter(_view, _model, _service);
        }

        [TearDown]
        public void Dispose()
        {
            _mockery.Dispose();
        }

        /// <summary>
        /// Tests the expectation if the view is dirty.
        /// </summary>
        [Test]
        public void TestSaveIfDirtyIsFalse()
        {
            Expect.Once.On(_view).GetProperty("IsDirty").Will(Return.Value(false));

            Assert.AreEqual(false, _presenter.Save());
            _mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// Test the behavior when saving valid data.
        /// </summary>
        [Test]
        public void TestSave()
        {
            //-- set all the exepectations
            Expect.Once.On(_view).GetProperty("IsDirty").Will(Return.Value(true));
            Expect.Once.On(_view).GetProperty("EmployeeID").Will(Return.Value(1));
            Expect.Once.On(_view).GetProperty("Lastname").Will(Return.Value("Cerrada"));
            Expect.Once.On(_view).GetProperty("Firstname").Will(Return.Value("Rod"));
            Expect.Once.On(_view).GetProperty("EmployeeType").Will(Return.Value(EnumEmployeeType.Admin));
            Expect.Once.On(_view).GetProperty("Salary").Will(Return.Value(1000.0));
            Expect.Once.On(_view).GetProperty("TAX").Will(Return.Value(0.15f));
            
            //-- service.Save should be called
            Expect.Once.On(_service).Method("Save").With(_model);

            Assert.AreEqual(true, _presenter.Save());
            _mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// Test the behavior when invalid data is entered. In this case lastname and first name is not provided.
        /// </summary>
        [Test]
        public void TestSaveWithError()
        {
            //-- set all the exepectations
            Expect.Once.On(_view).GetProperty("IsDirty").Will(Return.Value(true));
            Expect.Once.On(_view).GetProperty("EmployeeID").Will(Return.Value(1));
            Expect.Once.On(_view).GetProperty("Lastname").Will(Return.Value(""));//-- Raise error
            Expect.Once.On(_view).GetProperty("Firstname").Will(Return.Value("")); //-- Raise error
            Expect.Once.On(_view).GetProperty("EmployeeType").Will(Return.Value(EnumEmployeeType.Admin));
            Expect.Once.On(_view).GetProperty("Salary").Will(Return.Value(1000.0));
            Expect.Once.On(_view).GetProperty("TAX").Will(Return.Value(0.15f));

            //-- service.Save should be called
            Expect.Once.On(_view).Method("ShowValidationErrors").With(_model.ErrorMessages);

            //-- save should return false
            Assert.AreEqual(false, _presenter.Save());

            _mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// Test the behavior when the user confirmed the delete action.
        /// </summary>
        [Test]
        public void TestDeleteConfirmed()
        {
            Expect.Once.On(_view).Method("ConfirmDelete").Will(Return.Value(true));

            Expect.Once.On(_service).Method("Delete").With(1);

            Assert.AreEqual(true, _presenter.Delete(1));
            _mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// Test the behavior when the user did not confirmed the delete action.
        /// </summary>
        [Test]
        public void TestDeleteUnConfirmed()
        {
            Expect.Once.On(_view).Method("ConfirmDelete").Will(Return.Value(false));

            Assert.AreEqual(false, _presenter.Delete(1));
            _mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// Test the behavoir when the user close the view and the view is still dirty.
        /// </summary>
        [Test]
        public void TestCloseFormWhenDirty()
        {
            Expect.Once.On(_view).GetProperty("IsDirty").Will(Return.Value(true));
            Expect.Once.On(_view).Method("ConfirmClose").WithNoArguments().Will(Return.Value(true));
            Expect.Once.On(_view).Method("Close").WithNoArguments();

            _presenter.CloseForm(false);

            _mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// Test the behavoir when the user close the view and the view is NOT dirty.
        /// </summary>
        [Test]
        public void TestCloseFormWhenNotDirty()
        {
            Expect.Once.On(_view).GetProperty("IsDirty").Will(Return.Value(false));
            Expect.Once.On(_view).Method("Close").WithNoArguments();

            _presenter.CloseForm(false);
            _mockery.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
