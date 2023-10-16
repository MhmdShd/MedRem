import 'package:flutter/material.dart';
import 'package:mobile/models/reminder.dart';

class ExpenseItem extends StatelessWidget {
  const ExpenseItem(this.expense, {super.key});

  final Expense expense;
  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.symmetric(
          horizontal: 20,
          vertical: 16,
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              expense.name,
              style: Theme.of(context).textTheme.titleLarge,
            ),
            const SizedBox(height: 4),
            Row(
              children: [
                Text('${expense.dosage.toStringAsFixed(1)} mg'),
                const Spacer(),
                Row(
                  children: [
                    Tab(icon: categoryIcons[expense.categ]),
                    const SizedBox(
                      width: 8,
                    ),
                  ],
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
