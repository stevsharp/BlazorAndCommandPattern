using BlazorAppAndCommand.Models;
using BlazorAppAndCommand.Repositories;
using System.Collections.ObjectModel;

namespace BlazorAppAndCommand.Pages;

public partial class Home
{
    protected Product Product { get; set; }
    protected BlazorSearchModel SearchModel { get; set; } = new BlazorSearchModel();
    protected int Quantity { get; set; }

    protected IShoppingCartRepository shoppingCartRepository { get; set; } = new ShoppingCartRepository();
    protected IProductRepository productRepository { get; set; } = new ProductsRepository();

    public AddToCartCommand AddToCartCommand { get; private set; }
    public IUICommand IncreaseQuantityCommand { get; private set; }
    public IUICommand DecreaseQuantityCommand { get; private set; }
    public IUICommand RemoveFromCartCommand { get; private set; }

    private CommandManager manager = new CommandManager();

    public void Refresh()
    {
        //var products = productRepository
        //    .All()
        //    .Select(product => new ProductViewModel(this,
        //                            shoppingCartRepository,
        //                            productRepository,
        //                            product));

        //var lineItems = shoppingCartRepository
        //    .All()
        //    .Select(x => new ProductViewModel(this,
        //                        shoppingCartRepository,
        //                        productRepository,
        //                        x.Product,
        //                        x.Quantity));

        //Products = new ObservableCollection<ProductViewModel>(products);
        //LineItems = new ObservableCollection<ProductViewModel>(lineItems);

        //OnPropertyRaised(nameof(Products));
        //OnPropertyRaised(nameof(LineItems));
    }
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var increaseQuantityCommand = new ChangeQuantityCommand(ChangeQuantityCommand.Operation.Increase,
               shoppingCartRepository,
               productRepository,
               Product);

        var decreaseQuantityCommand =
            new ChangeQuantityCommand(ChangeQuantityCommand.Operation.Decrease,
                shoppingCartRepository,
                productRepository,
                Product);

        var removeFromCartCommand = new RemoveFromCartCommand(shoppingCartRepository,productRepository, Product);

        

        IncreaseQuantityCommand = new RelayCommand(
            execute: () => {
                increaseQuantityCommand.Execute();
                Refresh();
            },
            canExecute: () => increaseQuantityCommand.CanExecute());

        DecreaseQuantityCommand = new RelayCommand(
            execute: () => {
                decreaseQuantityCommand.Execute();
                Refresh();
            },
            canExecute: () => decreaseQuantityCommand.CanExecute());

        RemoveFromCartCommand = new RelayCommand(
            execute: () => {
                removeFromCartCommand.Execute();
                Refresh();
            },
            canExecute: () => removeFromCartCommand.CanExecute());
    }

    public void HandleFormSubmit()
    {
        // Do something with the form data
    }
    private void AddToCartCommandEvent()
    {
        var product = this.productRepository.FindBy("SM7B");

        AddToCartCommand = new AddToCartCommand(shoppingCartRepository, productRepository, product);

        manager.Invoke(AddToCartCommand);

        //AddToCartCommand = new RelayCommand(
        //   execute: () => {
        //       addToCartCommand.Execute();
        //       Refresh();
        //   },
        //   canExecute: () => addToCartCommand.CanExecute());

        //AddToCartCommand.Execute(product);
    }
}

