import 'package:mobile/Widgets/reminders.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:mobile/models/reminder.dart';
import 'package:mobile/Widgets/text_field.dart';
import 'package:mobile/Widgets/radio_card.dart';
import 'package:iconsax/iconsax.dart';

final formatter = DateFormat.yMd();

class NewReminder extends StatefulWidget {
  const NewReminder(this.onAddReminder, {super.key});
  final void Function(Reminder reminder) onAddReminder;
  @override
  State<StatefulWidget> createState() {
    return _newReminderState();
  }
}

class _newReminderState extends State<NewReminder> {
  Categories _selectedCategory = Categories.tablet;
  Frequency _selectedFrequency = Frequency.daily;
  final _nameController = TextEditingController();
  final _descriptionController = TextEditingController();
  final _dosageController = TextEditingController();
  final _frequencyController = TextEditingController();
  final _intervalController = TextEditingController();
  DateTime? _selectedDate;

  int getInterval(stopDate) {
    DateTime now = DateTime.now();
    return (stopDate.difference(now).inHours / 24).round();
  }

  void _presentDatePicker() async {
    final now = DateTime.now();
    final first = DateTime(now.year, now.month, now.day);
    final last = DateTime(now.year + 1, now.month, now.day);
    final pickedDate = await showDatePicker(
        context: context, initialDate: now, firstDate: first, lastDate: last);
    setState(() {
      _selectedDate = pickedDate;
    });
  }

  void _submitReminder() {
    final enteredAmount = double.tryParse(_dosageController.text);
    final amountIsInvalid = enteredAmount == null || enteredAmount <= 0;
    if (_nameController.text.trim().isEmpty ||
        amountIsInvalid ||
        _selectedDate == null) {
      showDialog(
        context: context,
        builder: (ctx) => AlertDialog(
          title: const Text('Invalid Input'),
          content: const Text('Please make sure valid data were input!'),
          actions: [
            TextButton(
                onPressed: () {
                  Navigator.pop(ctx);
                },
                child: const Text('Ok'))
          ],
        ),
      );
      return;
    }
    widget.onAddReminder(
      Reminder(
          name: _nameController.text,
          description: _descriptionController.text,
          dosage: double.parse(_dosageController.text),
          frequency: _selectedFrequency,
          interval: int.parse(getInterval(_selectedDate!).toString()),
          categ: _selectedCategory),
    );
    Navigator.pop(context);
  }

  @override
  void dispose() {
    _nameController.dispose();
    _descriptionController.dispose();
    _dosageController.dispose();
    _frequencyController.dispose();
    _intervalController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    Image pillIcon = Image.asset(
      categoryIcons[_selectedCategory]!,
      width: 25,
      height: 25,
    );
    ;
    return Padding(
      padding: const EdgeInsets.fromLTRB(16, 48, 16, 16),
      child: Column(
        children: [
          CustomTextField(
              customIcon: pillIcon, hint: 'Name', controller: _nameController),
          CustomTextField(
              customIcon: Image.asset(
                'assets/icons/note.png',
                width: 25,
                height: 25,
              ),
              hint: 'Description',
              controller: _descriptionController),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            mainAxisSize: MainAxisSize.max,
            children: [
              RadioCard(
                userIcon: 'assets/icons/capsule.png',
                iconWidth: 45,
                iconheight: 45,
                onTap: () {
                  setState(() {
                    _selectedCategory = Categories.capsule;
                  });
                },
                active: (_selectedCategory != Categories.capsule),
              ),
              RadioCard(
                userIcon: 'assets/icons/drop.png',
                iconWidth: 45,
                iconheight: 45,
                onTap: () {
                  setState(() {
                    _selectedCategory = Categories.drop;
                  });
                },
                active: (_selectedCategory != Categories.drop),
              ),
              RadioCard(
                userIcon: 'assets/icons/inhaler.png',
                iconWidth: 45,
                iconheight: 45,
                onTap: () {
                  setState(() {
                    _selectedCategory = Categories.inhaler;
                  });
                },
                active: (_selectedCategory != Categories.inhaler),
              ),
              RadioCard(
                userIcon: 'assets/icons/injection.png',
                iconWidth: 45,
                iconheight: 45,
                onTap: () {
                  setState(() {
                    _selectedCategory = Categories.injection;
                  });
                },
                active: (_selectedCategory != Categories.injection),
              ),
              RadioCard(
                userIcon: 'assets/icons/syrup.png',
                iconWidth: 45,
                iconheight: 45,
                onTap: () {
                  setState(() {
                    _selectedCategory = Categories.syrup;
                  });
                },
                active: (_selectedCategory != Categories.syrup),
              ),
              RadioCard(
                userIcon: 'assets/icons/tablets.png',
                iconWidth: 45,
                iconheight: 45,
                onTap: () {
                  setState(() {
                    _selectedCategory = Categories.tablet;
                  });
                },
                active: (_selectedCategory != Categories.tablet),
              ),
            ],
          ),
          Row(
            children: [
              Expanded(
                child: CustomTextField(
                  customIcon: Image.asset(
                    'assets/icons/weigh.png',
                    width: 25,
                    height: 25,
                  ),
                  inputType: TextInputType.number,
                  
                  controller: _dosageController,
                  label: 'mg',
                  hint: 'Dosage',
                ),
              ),
              const SizedBox(
                width: 16,
              ),
              Expanded(
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Text(_selectedDate == null
                        ? 'Reminder stop date'
                        : formatter.format(_selectedDate!)),
                    IconButton(
                        onPressed: _presentDatePicker,
                        icon: const Icon(Icons.calendar_month))
                  ],
                ),
              )
            ],
          ),
          const SizedBox(
            height: 16,
          ),
          Row(
            children: [
              DropdownButton(
                value: _selectedFrequency,
                items: Frequency.values
                    .map((e) => DropdownMenuItem(
                          value: e,
                          child: Text(e.name.toUpperCase()),
                        ))
                    .toList(),
                onChanged: (value) {
                  if (value == null) {
                    return;
                  }
                  setState(
                    () {
                      _selectedFrequency = value;
                    },
                  );
                },
              ),
              Expanded(
                  child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  ElevatedButton(
                    onPressed: () {
                      Navigator.pop(context);
                    },
                    child: const Text('Cancel'),
                  ),
                  ElevatedButton(
                    onPressed: _submitReminder,
                    child: const Text('Save Reminder'),
                  ),
                ],
              ))
            ],
          )
        ],
      ),
    );
  }
}
