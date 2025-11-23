const API_BASE = '/api';

class HotelApp {
    constructor() {
        // Добавляем небольшую задержку для инициализации DOM
        setTimeout(() => {
            this.init();
        }, 100);
    }

    async init() {
        console.log('HotelApp initializing...');

        try {
            await this.loadGuests();
            await this.loadRooms();
            await this.loadEmployees();
            await this.loadReservations();
            this.setupEventListeners();
            console.log('HotelApp initialized successfully');
        } catch (error) {
            console.error('Error initializing HotelApp:', error);
            this.showError('Ошибка загрузки данных');
        }
    }

    setupEventListeners() {
        const reservationForm = document.getElementById('reservationForm');
        const guestForm = document.getElementById('guestForm'); // ✅ ДОБАВИЛ

        if (reservationForm) {
            reservationForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.createReservation();
            });
        } else {
            console.error('Reservation form not found');
        }

        // ✅ ДОБАВИЛ обработчик для формы гостя
        if (guestForm) {
            guestForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.createGuest();
            });
        } else {
            console.error('Guest form not found');
        }
    }

    // ✅ ДОБАВИЛ метод создания гостя
    async createGuest() {
        const name = document.getElementById('guestName');
        const surname = document.getElementById('guestSurname');
        const phone = document.getElementById('guestPhone');
        const email = document.getElementById('guestEmail');

        if (!name || !surname || !phone) {
            alert('❌ Не все поля формы найдены');
            return;
        }

        const formData = {
            name: name.value.trim(),
            surname: surname.value.trim(),
            phone: phone.value.trim(),
            email: email.value.trim() || ''
        };

        console.log('📤 Отправляемые данные:', formData);

        // Валидация
        if (!formData.name || !formData.surname || !formData.phone) {
            alert('❌ Заполните обязательные поля: Имя, Фамилия, Телефон');
            return;
        }

        try {
            const response = await fetch(`${API_BASE}/Guests`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });

            console.log('📥 Ответ сервера:', response.status, response.statusText);

            if (response.ok) {
                const newGuest = await response.json();
                console.log('✅ Создан гость:', newGuest);

                document.getElementById('guestForm').reset();
                await this.loadGuests();
                document.getElementById('guestSelect').value = newGuest.id;
                alert('✅ Гость успешно добавлен!');
            } else {
                // Получим подробную ошибку от сервера
                const errorText = await response.text();
                console.error('❌ Ошибка сервера:', errorText);
                alert('❌ Ошибка при добавлении гостя: ' + errorText);
            }
        } catch (error) {
            console.error('💥 Ошибка:', error);
            alert('❌ Ошибка при добавлении гостя: ' + error.message);
        }
    }

    // Загрузка гостей для выпадающего списка
    async loadGuests() {
        try {
            console.log('Loading guests...');
            const response = await fetch(`${API_BASE}/Guests`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const guests = await response.json();
            console.log('Guests loaded:', guests);

            const select = document.getElementById('guestSelect');
            if (select) {
                select.innerHTML = '<option value="">Выберите гостя</option>';

                guests.forEach(guest => {
                    const option = document.createElement('option');
                    option.value = guest.id;
                    option.textContent = `${guest.name} ${guest.surname} (${guest.phone})`;
                    select.appendChild(option);
                });
            }
        } catch (error) {
            console.error('Ошибка загрузки гостей:', error);
            this.showError('Не удалось загрузить список гостей');
        }
    }

    // Загрузка комнат для выпадающего списка
    async loadRooms() {
        try {
            console.log('Loading rooms...');
            const response = await fetch(`${API_BASE}/Rooms`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const rooms = await response.json();
            console.log('Rooms loaded:', rooms);

            const select = document.getElementById('roomSelect');
            if (select) {
                select.innerHTML = '<option value="">Выберите комнату</option>';

                rooms.forEach(room => {
                    const option = document.createElement('option');
                    option.value = room.id;
                    option.textContent = `Комната ${room.id} (${room.floor} этаж, ${room.roomCapacity} чел.)`;
                    select.appendChild(option);
                });
            }
        } catch (error) {
            console.error('Ошибка загрузки комнат:', error);
            this.showError('Не удалось загрузить список комнат');
        }
    }

    // Загрузка сотрудников для выпадающего списка
    async loadEmployees() {
        try {
            console.log('Loading employees...');
            const response = await fetch(`${API_BASE}/Employees`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const employees = await response.json();
            console.log('Employees loaded:', employees);

            const select = document.getElementById('employeeSelect');
            if (select) {
                select.innerHTML = '<option value="">Выберите сотрудника</option>';

                employees.forEach(employee => {
                    const option = document.createElement('option');
                    option.value = employee.id;
                    option.textContent = `${employee.name} ${employee.surname}`;
                    select.appendChild(option);
                });
            }
        } catch (error) {
            console.error('Ошибка загрузки сотрудников:', error);
            this.showError('Не удалось загрузить список сотрудников');
        }
    }

    // Загрузка и отображение резерваций
    async loadReservations() {
        try {
            console.log('Loading reservations...');
            const response = await fetch(`${API_BASE}/Reservations`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const reservations = await response.json();
            console.log('Reservations loaded:', reservations);

            this.displayReservations(reservations);
        } catch (error) {
            console.error('Ошибка загрузки резерваций:', error);
            this.showError('Не удалось загрузить список резерваций');
        }
    }

    // Отображение списка резерваций
    displayReservations(reservations) {
        const container = document.getElementById('reservationsList');

        if (!container) {
            console.error('Reservations container not found');
            return;
        }

        if (!reservations || reservations.length === 0) {
            container.innerHTML = '<div class="loading">Нет активных резерваций</div>';
            return;
        }

        container.innerHTML = reservations.map(reservation => `
            <div class="reservation-card">
                <div class="reservation-header">
                    <span class="reservation-id">Резервация #${reservation.id}</span>
                    <span class="reservation-dates">
                        ${new Date(reservation.startDate).toLocaleDateString('ru-RU')} - ${new Date(reservation.endDate).toLocaleDateString('ru-RU')}
                    </span>
                </div>
                <div class="reservation-details">
                    <div class="detail-item">
                        <span class="detail-label">Гость:</span>
                        ${reservation.guest ? `${reservation.guest.name} ${reservation.guest.surname}` : 'N/A'}
                    </div>
                    <div class="detail-item">
                        <span class="detail-label">Комната:</span>
                        ${reservation.room ? `№${reservation.room.id}` : 'N/A'}
                    </div>
                    <div class="detail-item">
                        <span class="detail-label">Сотрудник:</span>
                        ${reservation.employee ? `${reservation.employee.name} ${reservation.employee.surname}` : 'N/A'}
                    </div>
                </div>
                <div class="reservation-actions">
                    <button class="btn btn-danger" onclick="app.deleteReservation(${reservation.id})">
                        Удалить
                    </button>
                </div>
            </div>
        `).join('');
    }

    // Создание новой резервации
    async createReservation() {
        const guestSelect = document.getElementById('guestSelect');
        const roomSelect = document.getElementById('roomSelect');
        const employeeSelect = document.getElementById('employeeSelect');
        const startDate = document.getElementById('startDate');
        const endDate = document.getElementById('endDate');

        if (!guestSelect || !roomSelect || !employeeSelect || !startDate || !endDate) {
            alert('Не все поля формы найдены');
            return;
        }

        const formData = {
            guestID: parseInt(guestSelect.value),
            roomID: parseInt(roomSelect.value),
            employeeID: parseInt(employeeSelect.value),
            startDate: startDate.value,
            endDate: endDate.value
        };

        // Валидация
        if (!formData.guestID || !formData.roomID || !formData.employeeID || !formData.startDate || !formData.endDate) {
            alert('Заполните все поля');
            return;
        }

        try {
            console.log('Creating reservation:', formData);

            const response = await fetch(`${API_BASE}/Reservations`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                // Очистка формы
                document.getElementById('reservationForm').reset();
                // Перезагрузка списка
                await this.loadReservations();
                alert('Резервация успешно создана!');
            } else {
                const errorText = await response.text();
                console.error('Server error:', errorText);
                alert('Ошибка при создании резервации: ' + response.status);
            }
        } catch (error) {
            console.error('Ошибка:', error);
            alert('❌ Ошибка при создании резервации: ' + error.message);
        }
    }

    // Удаление резервации
    async deleteReservation(id) {
        if (!confirm('Вы уверены, что хотите удалить эту резервацию?')) {
            return;
        }

        try {
            console.log('Deleting reservation:', id);

            const response = await fetch(`${API_BASE}/Reservations/${id}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                await this.loadReservations();
                alert('Резервация удалена!');
            } else {
                alert('Ошибка при удалении резервации: ' + response.status);
            }
        } catch (error) {
            console.error('Ошибка:', error);
            alert('Ошибка при удалении резервации: ' + error.message);
        }
    }

    // Показать ошибку
    showError(message) {
        const container = document.getElementById('reservationsList');
        if (container) {
            container.innerHTML = `<div class="error">${message}</div>`;
        }
    }
}

// Инициализация приложения когда DOM загружен
document.addEventListener('DOMContentLoaded', function () {
    window.app = new HotelApp();
});

// Глобальная функция для удаления (вызывается из HTML)
window.deleteReservation = function (id) {
    if (window.app) {
        window.app.deleteReservation(id);
    }
};