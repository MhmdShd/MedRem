import 'package:mobile/Widgets/chart/chart.dart';
import 'package:mobile/Widgets/new_reminder.dart';
import 'package:mobile/models/reminder.dart';
import 'package:flutter/material.dart';
import 'package:mobile/Widgets/reminder_list/reminder_list.dart';


class Expenses extends StatefulWidget {
  const Expenses({super.key});
  @override
  State<StatefulWidget> createState() {
    return _ExpensesState();
  }
}

class _ExpensesState extends State<Expenses> {
  final List<Expense> _registeredExpenses = [
    Expense(
      name: 'panadol',
      description: 'oiuwngw',
      frequency: Frequency.daily,
      dosage: 50,
      interval: 20,
      categ: Categories.tablet,
    ),
    Expense(
      name: 'panadol',
      frequency: Frequency.daily,
      interval: 20,
      description: 'oiuwngw',
      dosage: 50,
      categ: Categories.tablet,
    )
  ];

  void _addExpense(Expense expense) {
    setState(() {
      _registeredExpenses.add(expense);
    });
  }

  void _removeExpenses(Expense expense) {
    final expenseIndex = _registeredExpenses.indexOf(expense);
    setState(() {
      _registeredExpenses.remove(expense);
    });
    ScaffoldMessenger.of(context).clearSnackBars();
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        duration: const Duration(seconds: 3),
        content: const Text('Expense Deleted!'),
        action: SnackBarAction(
          label: 'Undo',
          onPressed: () {
            setState(() {
              _registeredExpenses.insert(expenseIndex, expense);
            });
          },
        ),
      ),
    );
  }

  void _openAddExpenseOverlay() {
    showModalBottomSheet(
      // isScrollControlled: true,
      context: context,
      isScrollControlled: true,
      builder: (ctx) => NewExpense(
        _addExpense,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    Widget mainContent = Center(
      child: Text('No Reminders Found!'),
    );
    if (_registeredExpenses.isNotEmpty) {
      mainContent = ExpensesList(
          expenses: _registeredExpenses, onRemoveExpense: _removeExpenses);
    }
    return Scaffold(
      appBar: AppBar(
        title: const Text('MedRem'),
        actions: [
          IconButton(
            onPressed: _openAddExpenseOverlay,
            icon: const Icon(Icons.add),
          )
        ],
      ),
      body: Column(
        children: [
          // Chart(expenses: _registeredExpenses),
          Expanded(
            child: mainContent,
          ),
        ],
      ),
    );
  }
}
