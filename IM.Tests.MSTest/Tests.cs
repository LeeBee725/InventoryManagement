using System;

using IM.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IM.Tests.MSTest
{
    // TODO WTS: Add appropriate tests
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        // TODO WTS: Add tests for functionality you add to ConsumptionViewModel.
        [TestMethod]
        public void TestConsumptionViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new ConsumptionViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to HomeViewModel.
        [TestMethod]
        public void TestHomeViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new HomeViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to InventoryViewModel.
        [TestMethod]
        public void TestInventoryViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new InventoryViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to RecipeViewModel.
        [TestMethod]
        public void TestRecipeViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new RecipeViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to SettingsViewModel.
        [TestMethod]
        public void TestSettingsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new SettingsViewModel();
            Assert.IsNotNull(vm);
        }
    }
}
