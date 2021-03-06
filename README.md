# BattleCity
Тестовый проект BattleCity на Unity 2020.3.2f1 и перенесён в 2020.3.15f2 (по условиям задания).

Взаимодействие с Git через модуль GitHub for Unity и GitHub Desktop


Проект выполнен в 3D с ортогональной камерой направленной строго вниз из-за чего создаётся иллюзия двухмерной игры, для управления персонажами использован старый InputManager. Передвижение персонажей и противников осуществляется через Rigidbody.

Возможна игра 1 или 2х человек (только на ПК, выбор осуществляется на сцене MainMenu (при запуске непосредственно игрового уровня возможна игра только за 1го человека без правки скрипта), для первого игрока управление WASD для второго стрелками, для этого в InputManager заданы дополнительные вертикальные и горизонтальные оси для персого и второго игрока) стрельба для первого игрока на пробел, для второго - enter на цифровой клавиатуре (кнопки стрельбы назначены в скрипте PlayerInputController потому как не смог задать в InputManager кнопку Enter цифровой панели).

Реализовано следующее:
1. Последовательный спавн противников. Противник спавнится как и в оригинальной игре в трёх точках в верхней линии в том же порядке (средняя, правая, левая) через равные промежутки времени (интервал спауна задётся в скрипте EnemySpawner в секундах). Там же задаются координаты точек спавна противников а так-же массив префабов противников для этого уровня. На данный момент противник выбирается случайным образом из массива что ведёт к большому количеству противника при попадании генерирующем ресурсы, поздне их количество необходимо снизить. Максимальное количество противников задано в скрипте SceneData и является общим для всех уровней.
2. Стрельба. Игрок(и) и противники могут стрелять. Мощность противников всегда равна 1 и их попадания всегда уничтожают игрока, другим противникам их снаряды не вредят (пролетают насквозь). У игроков, по умолчанию, мощность равна 1, этого достаточно для уничтожения простых танков противника а так-же кирпичных стен, так же как и снаряды противника снаряды игрока пролетают сквозь игрока (актуально при игре вдвоём). Игрок может подобрать бонус урона для увеличения своего урона. Цели для игнорирования задаются в скрипте Projectile в виде массива текстовых полей в которых необходимо указать тэги игнорируемых объектов. При уничтожении противника уничтожившему его игроку начисляются очки (количество очков задаётся в скрипте EnemyController)
3. Бонусы. Противники при попадании могут спавнить случайный бонус (массив бонусов задаётся в скрипте BonusSpawner) в случайном месте карты (границы спавна бонусов задаются двумя точками - верхней левой и правой нижней в скрипте BonusSpawner), для этого в скрипте EnemyController необходимо выбрать Spawn bonus. Одновременно на карте может находиться не более 1го бонуса (На данный момент при спавне нового бонуса старый объект уничтожается, новый спавнится через Instantiate, что не оптимально и в более крупном проекте это необходимо переделать на использование очереди (Queue) или не удалять старый объект а перемещать его за пределы видимости камеры и при необходимости переносить на новое место на карте, но т.к. проект маленький и постоянный спавн не особо влияет на его производительность, было оставлено именно в таком виде). Противники не могут брать бонусы.
4. Окончание игры. Игра оканчивается в следующих случаях:
   А. Потеря игроками всех жизней
   Б. Уничтожение базы противником или игроком
   В. Уничтожение игроками всех противников
В этих случаях игроку показывается финальный экран где выводятся очки набранные каждым игроком (если игрок был один то для второго игрока выводится только надпись - "Игрок 2", номер уровня и рекорд. (При запуске непосредственно игровой сцены номер уровня и рекорд всегда 0)
5. Стены. В данный момент в проекте всего 2 вида стен кирпичные и бронированные. Кирпичные стены имеют 1 жизнь, бронированные 2. Стена уничтожается в случае если у попавшего в неё игрока (или моба) урон равен или больше её прочности (прочность стен задаётся в скрипте Wall).
6. Уничтожение. Все объекты которые можно уничтожить должны наследовать от интерфейса IDestructable.
7. Регулировка громкости основных звуков (Звук начала уровня, звук окончания уровня) и звуковых эффектов (вызывается меню по кнопке P(З))

Что необходимо реализовать и оптимизмровать:
1. Систему локализации. Для её реализации планирую использовать словарь (Dictionary) где ключом будет id элемента для локализации, значение - текст локализации. Для хранения файлов локализации на диске использовать JSON или XML.
2. Улучшить "интеллект" ботов для более интересной игры и оптимизировать их спавн. На данный момент противники слишком "тупые", собираются в основном в верхней части карты, происходят глюки из-за столкновений с другими противниками, возможен спавн моба даже если точка спавна занята другим объектом.
3. Добавить большее количество бонусов
4. Добавить больше уровней.
5. Из предыдущего пункта следует что UI элементы игровой сцены будет оптимальным перенести на отдельную сцену и загружать иё как additional непосредственно к сцене уровня.
6. Добавить возможность создания и выбора начального уровня из списка уровней.
7. Оптимизировать спавн бонусов.
8. Оптимизировать звуковую систему.
9. Добавить на финальный экран уровня таймер по истечению которого загружать сцену главного меню (пока в проекте один уровень, в последствии запускать следующий уровень).
10. Написать лёгкие шейдеры для объектов сцены (текстура + цвет + GPU instancing)
11. Добавить оптимизированные спецэффекты (взрывы) на основе ParticleSystem. 

Почему появился список необходимого к реализации.
  ТЗ довольно условно и большая часть как по реализации так и по финальному результату отдан полностью на откуп программисту, в связи с чем улучшать, доделывать и оптимизировать проект можно практически бесконечно.
  В связи с этим было принято решение выложить проект который уже можно оценить, обозначить что, по моему мнению, стоит добавить или оптимизировать.
