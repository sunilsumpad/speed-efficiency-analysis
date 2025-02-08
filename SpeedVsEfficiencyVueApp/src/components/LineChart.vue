<template>
  <div class="full-width-wrapper">

    <!-- Page Header -->
    <header class="page-header">
      <h1>Motorbike Performance: Speed & Efficiency Analysis</h1>
    </header>

    <div class="container">
      <!-- Chart Section (70%) -->
      <div class="chart-section">
        <Line :data="chartData" :options="chartOptions" :key="chartKey" />
      </div>

      <!-- Table Section (30%) -->
      <div class="table-section">
        <div class="table-header">
          <h2>Data Table</h2>
          <button class="export-button" @click="exportToCSV">
            Export to CSV
          </button>
        </div>

        <!-- Scrollable Table Wrapper -->
        <div class="table-wrapper">
          <table class="data-table">
            <thead>
              <tr>
                <th>Date & Time</th>
                <th>Speed</th>
                <th>Efficiency</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(row, index) in tableData" :key="index">
                <td>{{ row.label }}</td>
                <td>{{ row.speed }}</td>
                <td>{{ row.mileage }}</td>
              </tr>
            </tbody>
          </table>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import { Line } from 'vue-chartjs';
import { Chart as ChartJS, Title, Tooltip, Legend, LineElement, PointElement, LinearScale, CategoryScale } from 'chart.js';
import "@/assets/line_chart.css";
import moment from 'moment';

ChartJS.register(Title, Tooltip, Legend, LineElement, PointElement, LinearScale, CategoryScale);

export default {
  // eslint-disable-next-line vue/no-reserved-component-names
  components: { Line },
  data() {
    return {
      chartData: {
        labels: [],
        datasets: []
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: true
      },
      chartKey: 0 // Used to force re-render
    };
  },
  methods: {
    fetchData() {
      this.getDataApiCall()
    },
    async getDataApiCall() {
      try {
        const response = await fetch('http://localhost:5178/api/BikeSpeedDataItems'); // Replace with your API URL
        const result = await response.json();
        let data = this.createLineChartData(result);
        /// Replace the entire object to ensure reactivity
        this.chartData = {
          labels: data.labels,
          datasets: [this.createSpeedDataset(data), this.createMileageDataset(data)]
        };
        // Force chart re-render
        this.chartKey++;
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    },
    createLineChartData(data) {
      let labels = [];
      let speedValues = [];
      let mileageValues = [];
      for (let i = 0; i < data.length; i++) {
        labels.push(moment(String(data[i].dateTime)).format('DD/MM hh:mm A'));
        speedValues.push(data[i].speed);
        mileageValues.push(data[i].mileage);
      }
      return { labels: labels, speedValues: speedValues, mileageValues: mileageValues };
    },
    createSpeedDataset(data) {
      return {
        label: 'Speed',
        backgroundColor: 'rgba(255, 99, 132, 0.2)',
        borderColor: 'rgba(255, 99, 132, 1)',
        data: data.speedValues
      }
    },
    createMileageDataset(data) {
      return {
        label: 'Efficiency',
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        borderColor: 'rgb(75, 192, 192, 1)',
        data: data.mileageValues
      }
    },
    exportToCSV() {
      // Convert table data into CSV format
      let csvContent = "data:text/csv;charset=utf-8,";
      csvContent += "Date & Time,Speed,Efficiency\n"; // CSV Header

      this.tableData.forEach(row => {
        csvContent += `${row.label},${row.speed},${row.mileage}\n`;
      });

      // Create a downloadable CSV file
      const encodedUri = encodeURI(csvContent);
      const link = document.createElement("a");
      link.setAttribute("href", encodedUri);
      link.setAttribute("download", "speed_efficiency_data.csv");
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  },
  computed: {
    tableData() {
      return this.chartData.labels.map((label, index) => ({
        label,
        speed: this.chartData.datasets[0]?.data[index] || 0,
        mileage: this.chartData.datasets[1]?.data[index] || 0
      }));
    }
  },
  mounted() {
    this.fetchData();
  },
  watch: {
    chartData: {
      deep: true,
      handler() {
        this.chartKey++;
      }
    }
  }
};
</script>
