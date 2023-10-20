import 'package:flutter/material.dart';
import 'package:mobile/models/reminder.dart';

class ReminderItem extends StatelessWidget {
  const ReminderItem(this.reminder, {super.key});

  final Reminder reminder;
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
              reminder.name,
              style: Theme.of(context).textTheme.titleLarge,
            ),
            const SizedBox(height: 4),
            Row(
              children: [
                Text('${reminder.dosage.toStringAsFixed(1)} mg'),
                const Spacer(),
                Row(
                  children: [
                    Tab(icon: Image.asset(categoryIcons[reminder.categ]!)),
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
