import machine
import dht
import time
import ssd1306
import socket
LED_PIN_NB = 2
led = machine.Pin(LED_PIN_NB, machine.Pin.OUT)
led.on()

button = machine.Pin(14, machine.Pin.IN, machine.Pin.PULL_UP)

ip = 'ip-here'
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

def connect_to_server():
    s.connect((ip, 5000))

# main
def display_content():
    try:
    	s.send('hello, button was pressed')

    except:
	print('socket err')

    i2c = machine.I2C(scl=machine.Pin(0), sda=machine.Pin(12))
    display = ssd1306.SSD1306_I2C(128, 64, i2c)

    dht22 = dht.DHT22(machine.Pin(4))
    dht22.measure()
    temp = dht22.temperature()
    humidity = dht22.humidity()

    display.fill(0)
    display.text('{:^16s}'.format('Temperature:'), 0, 0)
    display.text('{:^16s}'.format(str(temp)), 0, 16)
    display.text('{:^16s}'.format('Humidity'), 0, 32)
    display.text('{:^16s}'.format(str(humidity)), 0, 48)
    display.show()

def start_button():
    isDown = False

    while True:
	
	if button.value() == 0 and isDown == False:
	    print("button down")
	    isDown = True
	    # led.off()
	    display_content()

	elif button.value() == 1 and isDown == True:
	    isDown = False
	    # led.on()	
	time.sleep(0.01)

