import Link from 'next/link'
import Layout from '../components/Layout'

const IndexPage = () => (
  <Layout title="My Bank">
    <h1 className="text-blue-400">Hello My Bank</h1>
    <p>
      <Link href="/about">
        <a>About</a>
      </Link>
    </p>
  </Layout>
)

export default IndexPage
