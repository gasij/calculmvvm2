using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using calculmvvm2;

public class CalculatorViewModel : INotifyPropertyChanged
{
    private readonly CalculatorModel _model;
    private string _display;
    private double _currentValue;
    private string _currentOperation;

    public string Display
    {
        get => _display;
        set
        {
            _display = value;
            OnPropertyChanged();
        }
    }

    public CalculatorViewModel()
    {
        _model = new CalculatorModel();
        Display = "0";
    }

    public ICommand NumberCommand => new RelayCommand<string>(NumberPressed);
    public ICommand OperationCommand => new RelayCommand<string>(OperationPressed);
    public ICommand ClearCommand => new RelayCommand(Clear);
    public ICommand EqualsCommand => new RelayCommand(Calculate);

    private void NumberPressed(string number)
    {
        if (Display == "0")
            Display = number;
        else
            Display += number;
    }

    private void OperationPressed(string operation)
    {
        _currentValue = double.Parse(Display);
        _currentOperation = operation;
        Display = "0";
    }

    private void Calculate()
    {
        double secondValue = double.Parse(Display);
        double result = 0;

        switch (_currentOperation)
        {
            case "Add":
                result = _model.Add(_currentValue, secondValue);
                break;
            case "Subtract":
                result = _model.Subtract(_currentValue, secondValue);
                break;
            case "Multiply":
                result = _model.Multiply(_currentValue, secondValue);
                break;
            case "Divide":
                if (secondValue != 0)
                    result = _model.Divide(_currentValue, secondValue);
                else
                    Display = "Error"; 
                return; 
        }

        Display = result.ToString();
    }

    private void Clear()
    {
        Display = "0";
        _currentValue = 0;
        _currentOperation = null;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}



