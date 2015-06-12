namespace :semver do

  def read_semver
    File.open('.semver', &:readline).chomp
  end

  def parse_semver
    line = read_semver
    chunks = line.split "."

    return {
      :major => chunks[0].to_i,
      :minor => chunks[1].to_i,
      :patch => chunks[2].to_i
    }
  end

  def write_semver(version)
    major = version[:major]
    minor = version[:minor]
    patch = version[:patch]

    line = "#{major}.#{minor}.#{patch}"

    File.open('.semver', 'w') { |f| f.write(line) }
  end

  task "major" do
    version = parse_semver

    version[:major] += 1
    version[:minor] = 0
    version[:patch] = 0

    write_semver version
  end

  task "minor" do
    version = parse_semver

    version[:minor] += 1
    version[:patch] = 0

    write_semver version
  end

  task "patch" do
    version = parse_semver

    version[:patch] += 1

    write_semver version
  end

end
