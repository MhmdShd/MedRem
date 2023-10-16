import 'package:mobile/Widgets/new_reminder.dart';
import 'package:mobile/models/reminder.dart';
import 'package:flutter/material.dart';
import 'package:mobile/Widgets/reminder_list/reminder_list.dart';


class Reminders extends StatefulWidget {
  const Reminders({super.key});
  @override
  State<StatefulWidget> createState() {
    return _RemindersState();
  }
}

class _RemindersState extends State<Reminders> {
  final List<Reminder> _registeredReminders = [
    Reminder(
      name: 'panadol',
      description: 'oiuwngw',
      frequency: Frequency.daily,
      dosage: 50,
      interval: 20,
      categ: Categories.tablet,
    ),
    Reminder(
      name: 'panadol',
      frequency: Frequency.daily,
      interval: 20,
      description: 'oiuwngw',
      dosage: 50,
      categ: Categories.tablet,
    )
  ];

  void _addReminder(Reminder reminder) {
    setState(() {
      _registeredReminders.add(reminder);
    });
  }

  void _removeReminders(Reminder reminder) {
    final reminderIndex = _registeredReminders.indexOf(reminder);
    setState(() {
      _registeredReminders.remove(reminder);
    });
    ScaffoldMessenger.of(context).clearSnackBars();
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        duration: const Duration(seconds: 3),
        content: const Text('Reminder Deleted!'),
        action: SnackBarAction(
          label: 'Undo',
          onPressed: () {
            setState(() {
              _registeredReminders.insert(reminderIndex, reminder);
            });
          },
        ),
      ),
    );
  }

  void _openAddReminderOverlay() {
    showModalBottomSheet(
      // isScrollControlled: true,
      context: context,
      isScrollControlled: true,
      builder: (ctx) => NewReminder(
        _addReminder,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    Widget mainContent = Center(
      child: Text('No Reminders Found!'),
    );
    if (_registeredReminders.isNotEmpty) {
      mainContent = ReminderList(
          reminders: _registeredReminders, onRemoveReminder: _removeReminders);
    }
    return Scaffold(
      appBar: AppBar(
        title: const Text('MedRem'),
        actions: [
          IconButton(
            onPressed: _openAddReminderOverlay,
            icon: const Icon(Icons.add),
          )
        ],
      ),
      body: Column(
        children: [
          Expanded(
            child: mainContent,
          ),
        ],
      ),
    );
  }
}
