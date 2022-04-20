<template>
	<h1>Energy App</h1>
	<form @submit.prevent="add()">
		<label>Date </label>
		<input
			v-model="date"
			name="date"
			autocomplete="off"
			type="date"
		>
		<label>Energy Type </label>
		<select v-model="energy">
			<!-- <option>Gas</option>
			<option>Energy</option> -->
			<option v-for="et in energyType" :key="et" :value="et"> 
				{{et}}
			</option>
		</select>
		<label>Price </label>
		<input
			v-model="price"
			name="price"
			autocomplete="off"
			type="number"
		>
		<button>Add</button>
	</form>
	<h2>Energy Price List</h2>
	<ul>
		<li
			v-for="(obj, index) in energyPriceList"
			:key="index">
			<span>{{ obj.date }}</span>
			<span>{{ obj.energy }}</span>
			<span>{{ '$'+obj.price }}<span v-if="obj.isWeekEnd"> (10% discount)</span></span>
			<button @click="remove(index)">Remove</button>
		</li>
	</ul>
	<button v-if="energyPriceList.length > 0" @click="removeAll()">Remove All</button>
	<h4 v-if="energyPriceList.length === 0">Empty list.</h4>
</template>

<script>
	import { ref } from 'vue';
	export default {
		name: 'App',
		setup () {
			const date = ref('');
			const energy = ref('');
			const price = ref('');
			const defaultData = [{
				
				date: '',
				energy: '',
				price: 0
			}]
			const energyData = JSON.parse(localStorage.getItem('energyPriceList')) || defaultData;
			const energyPriceList = ref(energyData);
			function add () {
				if (date.value) {
					var today = new Date(date.value);
					let isWeekEnd = false;
					console.log(today);
					if(today.getDay() == 6 || today.getDay() == 0)
					{
						price.value = (price.value * 90)/100;
						isWeekEnd = true;
					}
					energyPriceList.value.push({
						date: date.value,
						energy: energy.value,
						price: price.value,
						isWeekEnd: isWeekEnd
					});
					date.value = '';
					price.value = 0;
					energy.value = '';
				}
				saveData();
			}

			function done () {
				
				saveData();
			}

			function remove (index) {
				energyPriceList.value.splice(index, 1);
				saveData();
			}

			function removeAll()
			{
				energyPriceList.value = [];
				localStorage.clear();
			}

			function saveData () {
				const storageData = JSON.stringify(energyPriceList.value);
				localStorage.setItem('energyPriceList', storageData);
			}

			return {
				energyPriceList,
				date,
				energy,
				price,
				add,
				done,
				remove,
				removeAll,
				saveData
			}
		},
		data() {
			return {
			energyType: [],
			};
		},
		methods: {
			async getData() {
				try {
					let response = await fetch("http://localhost:46169/Energy")
					this.energyType = await response.json();
					console.log(this.energyType);
					
				} catch (error) {
					console.log(error);
				}
			},
		},

		created() {
			this.getData();
		},
	}
</script>

<style lang="scss">
$border: 2px solid
	rgba(
		$color: white,
		$alpha: 0.35,
	);
$size1: 6px;
$size2: 12px;
$size3: 18px;
$size4: 24px;
$size5: 48px;
$backgroundColor: #27292d;
$textColor: white;
$primaryColor: #a0a4d9;
$secondTextColor: #1f2023;
body {
	margin: 0;
	padding: 0;
	font-family: Avenir, Helvetica, Arial, sans-serif;
	-webkit-font-smoothing: antialiased;
	-moz-osx-font-smoothing: grayscale;
	background-color: $backgroundColor;
	color: $textColor;
	#app {
		max-width: 600px;
		margin-left: auto;
		margin-right: auto;
		padding: 20px;
		h1 {
			font-weight: bold;
			font-size: 28px;
			text-align: center;
		}
		form {
			display: flex;
			flex-direction: column;
			width: 100%;
			label {
				font-size: 14px;
				font-weight: bold;
			}
			input,select,
			button {
				color-scheme: dark;
				height: $size5;
				box-shadow: none;
				outline: none;
				padding-left: $size2;
				padding-right: $size2;
				border-radius: $size1;
				font-size: 18px;
				margin-top: $size1;
				margin-bottom: $size2;
			}
			input {
				background-color: transparent;
				border: $border;
				color: inherit;
			}
		}
		button {
			cursor: pointer;
			background-color: $primaryColor;
			border: 1px solid $primaryColor;
			color: $secondTextColor;
			font-weight: bold;
			outline: none;
			border-radius: $size1;
		}
		h2 {
			font-size: 22px;
			border-bottom: $border;
			padding-bottom: $size1;
		}
		ul {
			padding: 10px;
			li {
				display: flex;
				justify-content: space-between;
				align-items: center;
				border: $border;
				padding: $size2 $size4;
				border-radius: $size1;
				margin-bottom: $size2;
				span {
					cursor: pointer;
					min-width: 150px;
				}
				.done {
					text-decoration: line-through;
				}
				button {
					font-size: $size2;
					padding: $size1;
				}
			}
		}
		h4 {
			text-align: center;
			opacity: 0.5;
			margin: 0;
		}
	}
}
</style>
