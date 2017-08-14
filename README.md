# testApp
Имеется ряд выявленных ошибок, не вынесенных в тесты:
1. Тип переменных GUID и IdLPU в документации - guid, по факту - string
2. Тип переменной /patient/Documents/DocumentDto/IdProvider в документации - int, по факту - string
3. /patient/Privilege - доступны поля, не указанные в документации (DisabilityDegree, IdDisabilityType, IsMain, MkbCode, PrivilegeDocument, Comment)
4. Опечатка в документации: " добавление пациента не просиходит" --> "добавление пациента не происходит"


Тесты создавались, исходя из фактического типа данных. 
