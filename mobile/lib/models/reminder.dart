import 'dart:math';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:uuid/uuid.dart';
import 'package:intl/intl.dart';

final formatter = DateFormat.yMd();

const uuid = Uuid();

enum Categories { syrup, tablet, capsule, drop, inhaler, injection }
enum Frequency { daily, weekly, monthly, yearly}

var categoryIcons = {
  Categories.syrup: 'assets/icons/syrup.png',
  Categories.tablet: 'assets/icons/tablets.png',
  Categories.capsule: 'assets/icons/capsule.png',
  Categories.drop: 'assets/icons/drop.png',
  Categories.inhaler: 'assets/icons/inhaler.png',
  Categories.injection: 'assets/icons/injection.png',
};

class Reminder {
  Reminder({
    required this.name,
    required this.description,
    required this.dosage,
    required this.frequency,
    required this.interval,
    required this.categ,

  }) : id = uuid.v4();

  final String id;
  final String name;
  final String description;
  final double dosage;
  final Frequency frequency;
  final int interval;
  final Categories categ;

}

class ReminderBucket {
  const ReminderBucket({
    required this.category,
    required this.reminders,
  });

  ReminderBucket.forCategory(
    List<Reminder> allReminders,
    this.category,
  ) 
  : reminders = allReminders
    .where((reminder) => reminder.categ == category).toList();
  final Categories category;
  final List<Reminder> reminders;

}
